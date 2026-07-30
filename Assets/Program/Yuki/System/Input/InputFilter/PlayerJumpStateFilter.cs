using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<PlayerJumpState> _state;

    public bool IsCanInput()
    {
        return _state.Any(state => _stateHolder.PlayerJumpState == state);
    }
}
