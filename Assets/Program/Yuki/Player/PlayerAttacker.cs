using UnityEngine;

/// <summary>
/// プレイヤーの攻撃用コンポーネント
/// </summary>
public class PlayerAttacker : MonoBehaviour
{
    /// <summary> 実行間隔のデータ </summary>
    [SerializeField]
    private IntervalTimeData _intervalData;
    /// <summary> 手裏剣のプレハブ </summary>
    [SerializeField]
    private GameObject _syurikenPrefab;
    /// <summary> 手裏剣を投げる中心 </summary>
    [SerializeField]
    private Transform _throwPoint;
    [SerializeField]
    private float _throwInterval;

    void Start()
    {
        _intervalData.Interval = _throwInterval;
    }

    public void ThrowSyuriken()
    {
        Instantiate(_syurikenPrefab, _throwPoint.position, _syurikenPrefab.transform.rotation);
    }
}
