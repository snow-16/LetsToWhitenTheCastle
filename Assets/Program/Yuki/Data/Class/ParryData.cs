using UnityEngine;

[CreateAssetMenu(fileName = "ParryData", menuName = "Scriptable Objects/ParryData")]
public class ParryData : ScriptableObject
{
    /// <summary> 攻撃のレイヤー </summary>
    [SerializeField]
    [Tooltip("攻撃と認識するレイヤーです。複数設定できます。")]
    private LayerMask _attackLayer;
    /// <summary> 攻撃のレイヤー </summary>
    public LayerMask AttackLayer => _attackLayer;

    /// <summary> パリィ受け付け時間 </summary>
    [SerializeField]
    [Tooltip("入力からパリィを受け付けている時間です")]
    private float _parryTime;
    /// <summary> パリィ受け付け時間 </summary>
    public float ParryTime => _parryTime;
}
