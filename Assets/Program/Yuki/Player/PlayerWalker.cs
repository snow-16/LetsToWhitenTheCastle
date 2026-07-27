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
    /// <summary> 空中制御力 </summary>
    [SerializeField]
    private float _airControl;

    /// <summary> 現在の速度 </summary>
    private float _nowSpeed = 0;
    /// <summary> 現在の移動方向 </summary>
    private float _nowDirection = 0;
    /// <summary> ダッシュしているか </summary>
    private bool _isSprint = false;

    /// <summary> SpriteRendererのインスタンス </summary>
    private SpriteRenderer _spriteRenderer;
    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerStateHolder = GetComponent<PlayerStateHolder>();
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
            else if(moveForword && Mathf.Abs(_nowSpeed) > maxSpeed)
            {
                _nowSpeed = maxSpeed * _nowDirection;
            }

            if(_playerStateHolder.PlayerJumpState != PlayerJumpState.OnGround)
            {
                _nowSpeed *= _airControl;
            }
        }
        else
        {
            _nowSpeed = Mathf.Max((1 - _damping) * Mathf.Abs(_nowSpeed), 0) * Mathf.Sign(_nowSpeed);
        }

        if(_nowSpeed != 0)
        {
            transform.Translate(_nowSpeed, 0, 0, Space.World);
        }
    }

    /// <summary>
    /// プレイヤーの向きを変更する
    /// </summary>
    private void FlipPlayer()
    {
        if(_nowDirection != 0)
        {
            var rot = transform.localEulerAngles;
            rot.y = _nowDirection > 0 ? 0 : 180;
            transform.localEulerAngles = rot;
        }
    }

    /// <summary>
    /// 移動開始
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void Walk(float direction)
    {
        _nowDirection += direction;
        FlipPlayer();
    }

    /// <summary>
    /// 移動停止
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void StopDirection(float direction)
    {
        _nowDirection -= direction;
        FlipPlayer();
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
