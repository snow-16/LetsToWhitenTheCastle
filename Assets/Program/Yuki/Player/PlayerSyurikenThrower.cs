using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの手裏剣攻撃用コンポーネント
/// </summary>
public class PlayerSyurikenThrower : MonoBehaviour
{
    /// <summary> 手裏剣のプレハブ </summary>
    [SerializeField]
    private GameObject _syurikenPrefab;
    /// <summary> 実行間隔のデータ </summary>
    [SerializeField]
    private IntervalTimeData _intervalData;
    /// <summary> 手裏剣が存在できる範囲 </summary>
    [SerializeField]
    [Tooltip("手裏剣が存在できる範囲を示すコライダーです。外に出たら消えます。")]
    private Collider2D _surviveArea;

    /// <summary> 障害物のレイヤー </summary>
    [SerializeField]
    [Tooltip("障害物と認識するレイヤーです。複数設定できます。")]
    private LayerMask _obstacleLayer;
    /// <summary> 手裏剣を投げる中心 </summary>
    [SerializeField]
    [Tooltip("手元の位置です。ここから投げます。")]
    private Transform _throwPoint;
    /// <summary> 手裏剣の投擲間隔 </summary>
    [SerializeField]
    [Tooltip("長押しの際の手裏剣を投げる間隔です。")]
    private float _throwInterval;
    /// <summary> 手裏剣の投擲速度 </summary>
    [SerializeField]
    [Tooltip("手裏剣を投げる速さです。")]
    private float _throwSpeed;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = GetComponent<PlayerStateHolder>();

        _intervalData.Interval = _throwInterval;
    }

    /// <summary>
    /// 手裏剣を投擲する
    /// </summary>
    public void ThrowSyuriken()
    {
        var syuriken = Instantiate(_syurikenPrefab, _throwPoint.position, _syurikenPrefab.transform.rotation).GetComponent<SyurikenMover>();
        syuriken.Throw(_throwSpeed, _surviveArea, _obstacleLayer);

        //手裏剣の攻撃判定付与
        this.ObserveEveryValueChanged(_ => syuriken.HitEnemy).Where(hit => hit).Subscribe(_ =>
            {
                _playerStateHolder.HitCount++;

                if(_playerStateHolder.HitCount == _playerStateHolder.BombGetBorder)
                {
                    _playerStateHolder.HitCount = 0;
                    _playerStateHolder.BombCount = Mathf.Min(_playerStateHolder.BombCount + 1, _playerStateHolder.MaxBombs);
                    Debug.Log("爆弾生成");
                }

                Destroy(syuriken.gameObject);
            }
        ).AddTo(syuriken);
    }
}
