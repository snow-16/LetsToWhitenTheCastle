using System;
using UnityEngine;

/// <summary>
/// ボスの出現アニメーションに基づいて入力をフィルタリングするフィルター
/// </summary>
[Serializable]
public class BossAnimationFilter : IInputFilter
{
    /// <summary> BossAIのインスタンス </summary>
    [SerializeField]
    private BossAI _bossAI;

    public bool IsCanInput()
    {
        return !_bossAI._isStartAnim;
    }
}
