using UnityEngine;

/// <summary>
/// プレイヤーの各種状態を保存するコンポーネント
/// </summary>
public class PlayerStateHolder : MonoBehaviour
{
    /// <summary> プレイヤーのジャンプ状態 </summary>
    public PlayerJumpState PlayerJumpState { get; set; } = PlayerJumpState.Fall;
    /// <summary> 手裏剣を当てた回数 </summary>
    public int HitCount { get; set; }
    /// <summary> 爆弾の所持数 </summary>
    public int BombCount { get; set; }
}
