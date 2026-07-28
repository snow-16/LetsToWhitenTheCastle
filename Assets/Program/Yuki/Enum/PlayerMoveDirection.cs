using System;

/// <summary>
/// プレイヤーのジャンプに関する状態
/// </summary>
[Flags]
public enum PlayerMoveDirection
{
    Right = 1 << 0,
    Left = 1 << 1,
}
