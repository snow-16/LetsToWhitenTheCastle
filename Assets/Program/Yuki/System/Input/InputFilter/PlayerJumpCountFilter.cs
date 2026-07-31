using System;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ状態に基づいて入力をフィルタリングするフィルター
/// </summary>
[Serializable]
public class PlayerJumpCountFilter : IInputFilter
{
    /// <summary> PlayerStateHolderのインスタンス </summary>
    [SerializeField]
    private PlayerStateHolder _stateHolder;

    public bool IsCanInput()
    {
        return _stateHolder.JumpCount > 0;
    }
}
