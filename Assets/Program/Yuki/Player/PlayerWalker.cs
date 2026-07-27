using System;
using UnityEngine;

/// <summary>
/// プレイヤーの横軸移動用コンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWalker : MonoBehaviour
{
    /// <summary> 各移動力の大きさの倍率。大きいほど細かく動く </summary>
    [SerializeField]
    private float _movingScale;
    /// <summary> 移動速度 </summary>
    [SerializeField]
    private WalkOrSprintProperty _speed;
    /// <summary> 最大移動速度 </summary>
    [SerializeField]
    private WalkOrSprintProperty _maxSpeed;
    /// <summary> 移動の初速 </summary>
    [SerializeField]
    private WalkOrSprintProperty _initialSpeed;
    /// <summary> 速度減衰量 </summary>
    [SerializeField]
    private float _damping;

    /// <summary> 現在の速度 </summary>
    private float _nowSpeed = 0;
    /// <summary> 現在の移動方向 </summary>
    private float _nowDirection = 0;
    /// <summary> ダッシュしているか </summary>
    private bool _isSprint = false;

    /// <summary> Rigidbody2Dのインスタンス </summary>
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(_nowDirection != 0)
        {
            var defaultSpeed = _speed.walk * (_isSprint ? _speed.sprintMultipiler : 1) / _movingScale;
            var maxSpeed = _maxSpeed.walk * (_isSprint ? _maxSpeed.sprintMultipiler : 1) / _movingScale;
            var initialSpeed = _initialSpeed.walk * (_isSprint ? _initialSpeed.sprintMultipiler : 1) / _movingScale;

            _nowSpeed += defaultSpeed * _nowDirection;
            var moveForword = _nowSpeed == 0 || Mathf.Sign(_nowSpeed) == _nowDirection;

            if(moveForword && Mathf.Abs(_nowSpeed) < initialSpeed)
            {
                _nowSpeed = initialSpeed * _nowDirection;
            }
            else if(moveForword && Mathf.Abs(_rigidbody2D.linearVelocityX) > _maxSpeed.walk * (_isSprint ? _maxSpeed.sprintMultipiler : 1))
            {
                _nowSpeed = maxSpeed * _nowDirection;
            }
        }
        else
        {
            _nowSpeed = Mathf.Max((1 - _damping) * Mathf.Abs(_nowSpeed), 0) * Mathf.Sign(_nowSpeed);
        }

        if(_nowSpeed != 0)
        {
            transform.Translate(_nowSpeed, 0, 0);
        }
    }

    /// <summary>
    /// 移動開始
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void Walk(float direction)
    {
        _nowDirection += direction;
    }

    /// <summary>
    /// 移動停止
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void StopDirection(float direction)
    {
        _nowDirection -= direction;
    }

    /// <summary>
    /// ダッシュ状態を切り替える
    /// </summary>
    public void SwitchSprint()
    {
        _isSprint = !_isSprint;
    }

    /// <summary>
    /// 速度の数値を歩きとダッシュに分けて保管する構造体
    /// </summary>
    [Serializable]
    private struct WalkOrSprintProperty
    {
        /// <summary> 歩き状態での速度 </summary>
        public float walk;
        /// <summary> ダッシュ状態での速度 </summary>
        public float sprintMultipiler;
    }
}
