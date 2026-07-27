using System;
using UnityEngine;

/// <summary>
/// 時間経過に基づいて入力をフィルタリングするフィルター
/// </summary>
[Serializable]
public class TimeFilter : IInputFilter
{
    /// <summary> 実行間隔のデータ </summary>
    [SerializeField]
    private IntervalTimeData _intervalData;

    /// <summary> 前回の実行からの経過時間 </summary>
    private float _progressTime;

    public bool IsCanInput()
    {
        _progressTime += Time.deltaTime;

        if(_progressTime >= _intervalData.Interval)
        {
            _progressTime = 0;
            return true;
        }

        return false;
    }
}
