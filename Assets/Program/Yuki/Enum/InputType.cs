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
}
