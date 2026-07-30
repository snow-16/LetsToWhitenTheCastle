using UnityEngine;

[CreateAssetMenu(fileName = "BombData", menuName = "Scriptable Objects/BombData")]
public class BombData : ScriptableObject
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

    /// <summary> 障害物のレイヤー </summary>
    [SerializeField]
    [Tooltip("着弾可能なレイヤーです。複数設定できます。")]
    private LayerMask _hitableLayer;
    /// <summary> 障害物のレイヤー </summary>
    public LayerMask HitableLayer => _hitableLayer;

    /// <summary> 爆弾の軌道の傾き </summary>
    [SerializeField]
    [Tooltip("爆弾の軌道のなだらかさです。")]
    private float _throwCurve;
    /// <summary> 爆弾の軌道の傾き </summary>
    public float ThrowCurve => _throwCurve;
    
    /// <summary> 爆弾を投げる高さ </summary>
    [SerializeField]
    [Tooltip("爆弾を投げる高さです。大きいほど山形になります。")]
    private float _throwHeight;
    /// <summary> 爆弾を投げる高さ </summary>
    public float ThrowHeight => _throwHeight;

    /// <summary> 爆弾の投擲距離 </summary>
    [SerializeField]
    [Tooltip("爆弾の投擲距離です。")]
    private float _throwLength;
    /// <summary> 爆弾の投擲距離 </summary>
    public float ThrowLength => _throwLength;

    /// <summary> 爆弾の投擲速度 </summary>
    [SerializeField]
    [Tooltip("爆弾の投擲速度です。")]
    private float _throwSpeed;
    /// <summary> 爆弾の投擲速度 </summary>
    public float ThrowSpeed => _throwSpeed;

    /// <summary> 爆弾生成に必要なヒット数 </summary>
    [SerializeField]
    [Tooltip("爆弾生成に必要な手裏剣のヒット数です。")]
    private int _collectLate;
    /// <summary> 爆弾生成に必要なヒット数 </summary>
    public int CollectLate => _collectLate;

    /// <summary> 爆弾の最大所持数 </summary>
    [SerializeField]
    [Tooltip("爆弾の最大所持数です。")]
    private int _maxBombs;
    /// <summary> 爆弾の最大所持数 </summary>
    public int MaxBombs => _maxBombs;
}
