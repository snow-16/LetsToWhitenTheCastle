using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの手裏剣攻撃用コンポーネント
/// </summary>
public class PlayerSyurikenThrower : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private SyurikenData _syurikenData;
    /// <summary> 設定データ </summary>
    [SerializeField]
    private BombData _bombData;
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
    /// <summary> 手裏剣を投げる中心 </summary>
    [SerializeField]
    [Tooltip("手元の位置です。ここから投げます。")]
    private Transform _throwPoint;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = GetComponent<PlayerStateHolder>();

        _intervalData.Interval = _syurikenData.ThrowInterval;
    }

    /// <summary>
    /// 手裏剣を投擲する
    /// </summary>
    public void ThrowSyuriken()
    {
        var syuriken = Instantiate(_syurikenPrefab, _throwPoint.position, _syurikenPrefab.transform.rotation).GetComponent<SyurikenMover>();
        syuriken.Throw(_syurikenData.ThrowSpeed, _surviveArea, _syurikenData.ObstacleLayer);

        //手裏剣の攻撃判定付与
        this.ObserveEveryValueChanged(_ => syuriken.HitEnemy).Where(hit => hit).Subscribe(_ =>
            {
                if(_playerStateHolder.BombCount < _bombData.MaxBombs)
                {
                    _playerStateHolder.HitCount++;

                    if(_playerStateHolder.HitCount == _bombData.CollectLate)
                    {
                        _playerStateHolder.HitCount = 0;
                        _playerStateHolder.BombCount++;
                        Debug.Log("爆弾生成");
                    }
                }

                Destroy(syuriken.gameObject);
            }
        ).AddTo(syuriken);
    }
}
