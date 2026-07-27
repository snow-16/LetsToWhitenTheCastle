using System;
using UnityEngine;

/// <summary>
/// プレイヤーの状態に基づいて入力をフィルタリングするフィルター
/// </summary>
[Serializable]
public class PlayerStateFilter : IInputFilter
{
    [SerializeField]
    private int test;

    public bool IsCanInput()
    {
        return true;
    }
}
