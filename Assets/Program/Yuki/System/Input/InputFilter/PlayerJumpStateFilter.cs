using System;
using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ状態に基づいて入力をフィルタリングするフィルター
/// </summary>
[Serializable]
public class PlayerJumpStateFilter : IInputFilter
{
    /// <summary> PlayerStateHolderのインスタンス </summary>
    [SerializeField]
    private PlayerStateHolder _stateHolder;
    /// <summary> 比較する状態 </summary>
    [SerializeField]
    private PlayerJumpState _state;

    public bool IsCanInput()
    {
        return _stateHolder.PlayerJumpState == _state;
    }
}
