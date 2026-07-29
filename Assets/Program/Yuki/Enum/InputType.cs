using System;

/// <summary>
/// キーバインドの種類
/// </summary>
[Flags]
public enum InputType
{
    /// <summary> 右移動キー </summary>
    Right = 1 << 0,
    /// <summary> 左移動キー </summary>
    Left = 1 << 1,
    /// <summary> ダッシュキー </summary>
    Sprint = 1 << 2,
    /// <summary> ジャンプキー </summary>
    Jump = 1 << 3,
    /// <summary> 手裏剣攻撃キー </summary>
    Syuriken = 1 << 4,
    /// <summary> 爆弾攻撃キー </summary>
    Bomb = 1 << 5,
    /// <summary> しゃがみ・床すり抜けキー </summary>
    Squat = 1 << 6,
    /// <summary> パリィキー </summary>
    Parry = 1 << 7,
}
