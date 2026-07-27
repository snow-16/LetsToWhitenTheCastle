using UnityEngine;

/// <summary>
/// プレイヤーの手裏剣攻撃用コンポーネント
/// </summary>
public class PlayerSyurikenThrower : MonoBehaviour
{
    /// <summary> 実行間隔のデータ </summary>
    [SerializeField]
    private IntervalTimeData _intervalData;
    /// <summary> 手裏剣のプレハブ </summary>
    [SerializeField]
    private GameObject _syurikenPrefab;
    /// <summary> 手裏剣が存在できる範囲 </summary>
    [SerializeField]
    private Collider2D _surviveArea;
    /// <summary> 手裏剣を投げる中心 </summary>
    [SerializeField]
    private Transform _throwPoint;
    /// <summary> 手裏剣の投擲間隔 </summary>
    [SerializeField]
    private float _throwInterval;
    /// <summary> 手裏剣の投擲速度 </summary>
    [SerializeField]
    private float _throwSpeed;

    void Start()
    {
        _intervalData.Interval = _throwInterval;
    }

    /// <summary>
    /// 手裏剣を投擲する
    /// </summary>
    public void ThrowSyuriken()
    {
        var syuriken = Instantiate(_syurikenPrefab, _throwPoint.position, _syurikenPrefab.transform.rotation).GetComponent<SyurikenMover>();
        syuriken.Throw(_throwSpeed, transform.right, _surviveArea);
    }
}
