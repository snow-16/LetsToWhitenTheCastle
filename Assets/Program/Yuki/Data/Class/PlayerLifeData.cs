using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLifeData", menuName = "Scriptable Objects/PlayerLifeData")]
public class PlayerLifeData : ScriptableObject
{
    /// <summary> 最大HP </summary>
    [SerializeField]
    [Tooltip("最大HPです。")]
    private int _maxHP;
    /// <summary> 最大HP </summary>
    public int MaxHP { get => _maxHP; set => _maxHP = value; }

    /// <summary> 無敵時間 </summary>
    [SerializeField]
    [Tooltip("無敵時間です。")]
    private float _invincibleTime;
    /// <summary> 無敵時間 </summary>
    public float InvincibleTime { get => _invincibleTime; set => _invincibleTime = value; }
}
