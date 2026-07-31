using UnityEngine;

[CreateAssetMenu(fileName = "SyurikenData", menuName = "Scriptable Objects/SyurikenData")]
public class SyurikenData : ScriptableObject
{
    /// <summary> 基礎ダメージ </summary>
    [SerializeField]
    [Tooltip("基準となるダメージ量です。")]
    private int _baseDamage;
    /// <summary> 基礎ダメージ </summary>
    public int BaseDamage => _baseDamage;

    /// <summary> 距離ダメージ倍率 </summary>
    [SerializeField]
    [Tooltip("距離によるダメージ倍率です。")]
    private float _distanceMultiplier;
    /// <summary> 距離ダメージ倍率 </summary>
    public float DistanceMultiplier => _distanceMultiplier;

    /// <summary> 高度ダメージ倍率 </summary>
    [SerializeField]
    [Tooltip("高度によるダメージ倍率です。")]
    private float _heightMultiplier;
    /// <summary> 高度ダメージ倍率 </summary>
    public float HeightMultiplier => _heightMultiplier;

    /// <summary> 障害物のレイヤー </summary>
    [SerializeField]
    [Tooltip("障害物と認識するレイヤーです。複数設定できます。")]
    private LayerMask _obstacleLayer;
    /// <summary> 障害物のレイヤー </summary>
    public LayerMask ObstacleLayer => _obstacleLayer;

    /// <summary> 手裏剣の投擲間隔 </summary>
    [SerializeField]
    [Tooltip("長押しの際の手裏剣を投げる間隔です。")]
    private float _throwInterval;
    /// <summary> 手裏剣の投擲間隔 </summary>
    public float ThrowInterval => _throwInterval;

    /// <summary> 手裏剣の投擲速度 </summary>
    [SerializeField]
    [Tooltip("手裏剣を投げる速さです。")]
    private float _throwSpeed;
    /// <summary> 手裏剣の投擲速度 </summary>
    public float ThrowSpeed => _throwSpeed;
}
