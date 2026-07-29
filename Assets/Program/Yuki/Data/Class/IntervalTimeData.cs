using UnityEngine;

/// <summary>
/// タイマーの間隔を設定するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "IntervalTimeData", menuName = "Scriptable Objects/IntervalTimeData")]
public class IntervalTimeData : ScriptableObject
{
    /// <summary> 実行間隔 </summary>
    [SerializeField]
    private float _interval;
    /// <summary> 実行間隔 </summary>
    public float Interval { get; set; }
}
