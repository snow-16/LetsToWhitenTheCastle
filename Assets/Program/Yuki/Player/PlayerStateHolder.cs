using UnityEngine;

/// <summary>
/// プレイヤーの各種状態を保存するコンポーネント
/// </summary>
public class PlayerStateHolder : MonoBehaviour
{
    /// <summary> プレイヤーのジャンプ状態 </summary>
    public PlayerJumpState PlayerJumpState { get; set; } = PlayerJumpState.Fall;
}
