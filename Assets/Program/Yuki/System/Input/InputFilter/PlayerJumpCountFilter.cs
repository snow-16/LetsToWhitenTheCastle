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
    /// <summary> 設定データ </summary>
    [SerializeField]
    private PlayerJumpdata _jumpData;

    public bool IsCanInput()
    {
        return _stateHolder.JumpCount < _jumpData.JumpableCount - 1;
    }
}
