using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWalkdata", menuName = "Scriptable Objects/PlayerWalkdata")]
public class PlayerWalkdata : ScriptableObject
{
    /// <summary> 移動速度 </summary>
    [SerializeField]
    [Tooltip("加速力です。")]
    private WalkOrSprintProperty _speed;
    /// <summary> 移動速度 </summary>
    public WalkOrSprintProperty Speed => _speed;
    
    /// <summary> 速度減衰量 </summary>
    [SerializeField]
    [Tooltip("横方向の速度減衰率です。〜1で設定してください。")]
    private float _damping;
    /// <summary> 速度減衰量 </summary>
    public float Damping => _damping;

    /// <summary> 空中制御力 </summary>
    [SerializeField]
    [Tooltip("空中での速度減衰率です。〜1で設定してください。")]
    private float _airControl;
    /// <summary> 空中制御力 </summary>
    public float AirControl => _airControl;

    /// <summary>
    /// 速度の数値を歩きとダッシュに分けて保管する構造体
    /// </summary>
    [Serializable]
    public struct WalkOrSprintProperty
    {
        /// <summary> 歩き状態での速度 </summary>
        [Tooltip("歩き状態での速度です。")]
        public float walk;
        /// <summary> ダッシュ状態での速度 </summary>
        [Tooltip("ダッシュ状態での速度です。")]
        public float sprintMultipiler;
    }
}
