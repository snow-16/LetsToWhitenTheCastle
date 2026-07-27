using System;

/// <summary>
/// キーバインドの種類
/// </summary>
[Flags]
public enum InputType
{
    /// <summary> 横移動キー </summary>
    Go = 1 << 0,
    /// <summary> ダッシュキー </summary>
    Sprint = 1 << 1,
    /// <summary> ジャンプキー </summary>
    Jump = 1 << 2,
}
