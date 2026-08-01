using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// プレイヤーの爆弾攻撃用コンポーネント
/// </summary>
public class PlayerBombThrower : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private BombData _bombData;
    /// <summary> 爆弾のプレハブ </summary>
    [SerializeField]
    private GameObject _bombPrefab;
    /// <summary> 着弾予測地点表示 </summary>
    [SerializeField]
    private GameObject _targetPoiner;
    /// <summary> 爆弾を投げる中心 </summary>
    [SerializeField]
    private Transform _throwPoint;
    /// <summary> 高度に応じて放物線の色をどれだけ変えるか </summary>
    [SerializeField]
    private float _throwLineGradationLevel;

    /// <summary> 投擲軌道の計算関数 </summary>
    private Func<float, Vector2> _throwFunc;

    /// <summary> LineRendererのインスタンス </summary>
    private LineRenderer _lineRenderer;
    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _playerStateHolder = GetComponent<PlayerStateHolder>();
    }

    void FixedUpdate()
    {
        if(_playerStateHolder.BombCount > 0)
        {
            var zeroPoint = -Mathf.Abs(Mathf.Sqrt((0 - _bombData.ThrowHeight) / -_bombData.ThrowCurve));
            var basePoint = _throwPoint.position;
            _throwFunc = distance =>
            {
                var x = zeroPoint + _bombData.ThrowLength * distance;

                return (Vector2)basePoint + new Vector2(x - zeroPoint, -_bombData.ThrowCurve * Mathf.Pow(x, 2) + _bombData.ThrowHeight);
            };

            var onGround = _playerStateHolder.PlayerJumpState == PlayerJumpState.OnGround || _playerStateHolder.PlayerJumpState == PlayerJumpState.Coyote;
            _lineRenderer.positionCount = 30;
            var heightColorList = new List<Color>();
            var alphaMap = new GradientAlphaKey[2];
            alphaMap[0] = new GradientAlphaKey(onGround ? 0.8f : 0.3f, 0);
            alphaMap[1] = new GradientAlphaKey(onGround ? 0.8f : 0.3f, 1);
            var setColorInterval = (_lineRenderer.positionCount - (_lineRenderer.positionCount % 7)) / 7;
            
            for(int i = 0; i < _lineRenderer.positionCount; i++)
            {
                var startPos = i == 0 ? (Vector2)_throwPoint.position : _throwFunc(i - 1);
                var endPos = _throwFunc(i);

                if(i % setColorInterval == 0)
                {
                    var gradationLevel = startPos.y / _throwLineGradationLevel;
                    heightColorList.Add(new Color(1, Mathf.Max(1 - gradationLevel, 0), Mathf.Max(1 - gradationLevel, 0), 1));
                }

                var predictionCast = Physics2D.Linecast(startPos, endPos, _bombData.HitableLayer);
                if(predictionCast)
                {
                    _lineRenderer.SetPosition(i, predictionCast.point);
                    _lineRenderer.positionCount = i + 1;
                    _targetPoiner.SetActive(true);
                    _targetPoiner.transform.position = predictionCast.point;
                    var gradationLevel = predictionCast.point.y / _throwLineGradationLevel;
                    heightColorList.Add(new Color(1, Mathf.Max(1 - gradationLevel, 0), Mathf.Max(1 - gradationLevel, 0), 1));
                    break;
                }
                else
                {
                    _lineRenderer.SetPosition(i, endPos);
                }
            }

            Gradient gradient = new();
            var heightColorMap = new List<GradientColorKey>();

            for(int i = 0; i < heightColorList.Count; i++)
            {
                heightColorMap.Add(new GradientColorKey(heightColorList[i], i / (heightColorList.Count - 1f)));
            }
            
            gradient.SetKeys(heightColorMap.ToArray(), alphaMap);
            _lineRenderer.colorGradient = gradient;
        }
        else
        {
            _lineRenderer.positionCount = 0;
            _targetPoiner.SetActive(false);
        }
    }

    /// <summary>
    /// 爆弾を投擲する
    /// </summary>
    public void ThrowBomb()
    {
        if(_playerStateHolder.PlayerJumpState == PlayerJumpState.OnGround || _playerStateHolder.PlayerJumpState == PlayerJumpState.Coyote)
        {
            var bomb = Instantiate(_bombPrefab, _throwPoint.position, _bombPrefab.transform.rotation).GetComponent<BombMover>();
            bomb.Throw(_throwFunc, _bombData.ThrowSpeed, _bombData.HitableLayer);
            _playerStateHolder.BombCount--;
            SoundSystem.Instance.PlaySE(SEType.ThrowBomb);
        }
    }
}
