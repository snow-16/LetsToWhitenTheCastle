using System;
using UnityEngine;

/// <summary>
/// プレイヤーの爆弾攻撃用コンポーネント
/// </summary>
public class PlayerBombThrower : MonoBehaviour
{
    /// <summary> 爆弾のプレハブ </summary>
    [SerializeField]
    private GameObject _bombPrefab;
    /// <summary> 障害物のレイヤー </summary>
    [SerializeField]
    [Tooltip("着弾可能なレイヤーです。複数設定できます。")]
    private LayerMask _hitableLayer;
    /// <summary> 爆弾の軌道の傾き </summary>
    [SerializeField]
    [Tooltip("爆弾の軌道のなだらかさです。")]
    private float _throwCurve;
    /// <summary> 爆弾を投げる高さ </summary>
    [SerializeField]
    [Tooltip("爆弾を投げる高さです。大きいほど山形になります。")]
    private float _throwHeight;
    /// <summary> 爆弾を投げる中心 </summary>
    [SerializeField]
    [Tooltip("手元の位置です。ここから投げます。")]
    private Transform _throwPoint;
    /// <summary> 爆弾の投擲距離 </summary>
    [SerializeField]
    [Tooltip("爆弾の投擲距離です。")]
    private float _throwLength;
    /// <summary> 爆弾の投擲速度 </summary>
    [SerializeField]
    [Tooltip("爆弾の投擲速度です。")]
    private float _throwSpeed;

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
            var zeroPoint = -Mathf.Abs(Mathf.Sqrt((0 - _throwHeight) / -_throwCurve));
            var basePoint = _throwPoint.position;
            _throwFunc = distance =>
            {
                var x = zeroPoint + _throwLength * distance;

                return (Vector2)basePoint + new Vector2(x - zeroPoint, -_throwCurve * Mathf.Pow(x, 2) + _throwHeight);
            };

            _lineRenderer.positionCount = 30;
            
            for(int i = 0; i < 30; i++)
            {
                var startPos = i == 0 ? (Vector2)_throwPoint.position : _throwFunc(i - 1);
                var endPos = _throwFunc(i);

                var predictionCast = Physics2D.Linecast(startPos, endPos, _hitableLayer);
                if(predictionCast)
                {
                    _lineRenderer.SetPosition(i, predictionCast.point);
                    _lineRenderer.positionCount = i + 1;
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
        }
    }

    /// <summary>
    /// 爆弾を投擲する
    /// </summary>
    public void ThrowBomb()
    {
        var bomb = Instantiate(_bombPrefab, _throwPoint.position, _bombPrefab.transform.rotation).GetComponent<BombMover>();
        bomb.Throw(_throwFunc, _throwSpeed, _hitableLayer);
    }
}
