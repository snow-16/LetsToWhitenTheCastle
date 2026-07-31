using System;
using UnityEngine;

/// <summary>
/// プレイヤーの爆弾所持数に基づいて入力をフィルタリングするフィルター
/// </summary>
[Serializable]
public class PlayerBombCountFilter : IInputFilter
{
    /// <summary> PlayerStateHolderのインスタンス </summary>
    [SerializeField]
    private PlayerStateHolder _stateHolder;

    public bool IsCanInput()
    {
        return _stateHolder.BombCount > 0;
    }
}
