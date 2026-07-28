using UnityEngine;

/// <summary>
/// プレイヤーの爆弾攻撃用コンポーネント
/// </summary>
public class PlayerBombThrower : MonoBehaviour
{
    /// <summary> 爆弾のプレハブ </summary>
    [SerializeField]
    private GameObject _bombPrefab;
    /// <summary> 爆弾の最低着弾地点 </summary>
    [SerializeField]
    [Tooltip("爆弾を投げる最低位置です。最高位置との線分上に投げます。")]
    private Transform _bombLowTargetPoint;
    /// <summary> 爆弾の最高着弾地点 </summary>
    [SerializeField]
    [Tooltip("爆弾を投げる最高位置です。最低位置との線分上に投げます。")]
    private Transform _bombHighTargetPoint;
    /// <summary> 爆弾の高度が上がり始める距離 </summary>
    [SerializeField]
    [Tooltip("爆弾の高度が上がり始める距離です。最低位置を基準にします。")]
    private float _bombTargetingLength;
    /// <summary> 爆弾を投げる中心 </summary>
    [SerializeField]
    [Tooltip("手元の位置です。ここから投げます。")]
    private Transform _throwPoint;
    /// <summary> 爆弾を投げる高さ </summary>
    [SerializeField]
    [Tooltip("爆弾を投げる高さです。大きいほど山形になります。")]
    private float _throwHeight;
    /// <summary> 爆弾の投擲速度 </summary>
    [SerializeField]
    [Tooltip("爆弾の投擲速度です。")]
    private float _throwSpeed;

    /// <summary> 現在の爆弾投擲予測地点 </summary>
    private Vector2 _bombTargetPoint;

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
            var throwHeight = Mathf.Max(_bombTargetingLength - (_bombLowTargetPoint.position - transform.position).magnitude, 0) / _bombTargetingLength;
            var start = (Vector2)_throwPoint.position;
            var end = _bombTargetPoint = Vector2.Lerp(_bombLowTargetPoint.position, _bombHighTargetPoint.position, throwHeight);
            _lineRenderer.positionCount = 10;
            
            for(int i = 0; i < 10; i++)
            {
                var basePos = Vector2.Lerp(start, end, i / 9f);
                basePos.y += Mathf.Sin(i / 9f * Mathf.PI) * (end - start).magnitude * _throwHeight;
                _lineRenderer.SetPosition(i, basePos);
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
        bomb.Throw(_throwSpeed, _bombTargetPoint, _throwHeight);
    }
}
