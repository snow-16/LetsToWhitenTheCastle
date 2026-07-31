using System;
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

    /// <summary> 投擲軌道の計算関数 </summary>
    private Func<float, Vector2> _throwFunc;
    /// <summary> 爆弾を構えているか </summary>
    private bool _isTargeting = false;

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

            _lineRenderer.positionCount = 30;
            _lineRenderer.startColor = _lineRenderer.endColor = new(1, 1, 1, _isTargeting ? 0.5f : 0.1f);
            
            for(int i = 0; i < 30; i++)
            {
                var startPos = i == 0 ? (Vector2)_throwPoint.position : _throwFunc(i - 1);
                var endPos = _throwFunc(i);

                var predictionCast = Physics2D.Linecast(startPos, endPos, _bombData.HitableLayer);
                if(predictionCast)
                {
                    _lineRenderer.SetPosition(i, predictionCast.point);
                    _lineRenderer.positionCount = i + 1;
                    _targetPoiner.SetActive(true);
                    _targetPoiner.transform.position = predictionCast.point;
                    break;
                }
                else
                {
                    _lineRenderer.SetPosition(i, endPos);
                }
            }
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
        if(_isTargeting)
        {
            var bomb = Instantiate(_bombPrefab, _throwPoint.position, _bombPrefab.transform.rotation).GetComponent<BombMover>();
            bomb.Throw(_throwFunc, _bombData.ThrowSpeed, _bombData.HitableLayer);
            _playerStateHolder.BombCount--;
        }
    }

    /// <summary>
    /// 爆弾を構えているかを変更する
    /// </summary>
    /// <param name="isTargeting"></param>
    public void SwitchTargeting(bool isTargeting)
    {
        _isTargeting = isTargeting;
    }
}
