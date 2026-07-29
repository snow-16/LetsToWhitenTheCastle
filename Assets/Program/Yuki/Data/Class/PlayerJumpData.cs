using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJumpdata", menuName = "Scriptable Objects/PlayerJumpdata")]
public class PlayerJumpdata : ScriptableObject
{
    /// <summary> ジャンプの初速 </summary>
    [SerializeField]
    [Tooltip("ジャンプの初速です。")]
    private float _initialJumpPower;
    /// <summary> ジャンプの初速 </summary>
    public float InitialJumpPower => _initialJumpPower;

    /// <summary> ジャンプの伸び率 </summary>
    [SerializeField]
    [Tooltip("長押しでの上昇力です。")]
    private float _keepJumpPower;
    /// <summary> ジャンプの伸び率 </summary>
    public float KeepJumpPower => _keepJumpPower;

    /// <summary> ジャンプの最大距離 </summary>
    [SerializeField]
    [Tooltip("ジャンプの最大飛距離です。")]
    private float _maxJump;
    /// <summary> ジャンプの最大距離 </summary>
    public float MaxJump => _maxJump;
}
