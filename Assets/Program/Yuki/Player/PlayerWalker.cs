using System;
using UnityEngine;

/// <summary>
/// プレイヤーの横軸移動用コンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWalker : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private PlayerWalkdata _walkData;
    /// <summary> 各移動力の大きさの倍率。大きいほど細かく動く </summary>
    [SerializeField]
    private float _movingScale;

    /// <summary> 現在の速度 </summary>
    private float _nowSpeed = 0;
    /// <summary> 現在の移動方向 </summary>
    private PlayerMoveDirection _nowDirection;
    /// <summary> ダッシュしているか </summary>
    private bool _isSprint = false;
    /// <summary> 方向転換が無効化されているか </summary>
    private bool _isLockFlip = false;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = GetComponent<PlayerStateHolder>();
    }

    void FixedUpdate()
    {
        if(_nowDirection != 0)
        {
            var defaultSpeed = _walkData.Speed.walk * (_isSprint ? _walkData.Speed.sprintMultipiler : 1) / _movingScale;
            var maxSpeed = _walkData.MaxSpeed.walk * (_isSprint ? _walkData.MaxSpeed.sprintMultipiler : 1) / _movingScale;
            var initialSpeed = _walkData.InitialSpeed.walk * (_isSprint ? _walkData.InitialSpeed.sprintMultipiler : 1) / _movingScale;

            _nowSpeed += defaultSpeed * GetDirection();
            var moveForword = _nowSpeed == 0 || Mathf.Sign(_nowSpeed) == GetDirection();

            if(moveForword && Mathf.Abs(_nowSpeed) < initialSpeed)
            {
                _nowSpeed = initialSpeed * GetDirection();
            }
            else if(moveForword && Mathf.Abs(_nowSpeed) > maxSpeed)
            {
                _nowSpeed = maxSpeed * GetDirection();
            }

            if(_playerStateHolder.PlayerJumpState != PlayerJumpState.OnGround)
            {
                _nowSpeed *= _walkData.AirControl;
            }
        }
        else
        {
            _nowSpeed = Mathf.Max((1 - _walkData.Damping) * Mathf.Abs(_nowSpeed), 0) * Mathf.Sign(_nowSpeed);
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
        if(GetDirection() != 0 && !_isLockFlip)
        {
            var rot = transform.localEulerAngles;
            rot.y = GetDirection() > 0 ? 0 : 180;
            transform.localEulerAngles = rot;
        }
    }

    /// <summary>
    /// PlayerMoveDirectionからint型の方向へ変換する
    /// </summary>
    /// <returns>進行方向</returns>
    private int GetDirection()
    {
        int direction = 0;
        
        if((_nowDirection & PlayerMoveDirection.Right) != 0)
        {
            direction += 1;
        }
        if((_nowDirection & PlayerMoveDirection.Left) != 0)
        {
            direction -= 1;
        }

        return direction;
    }

    /// <summary>
    /// 移動開始
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void Walk(float direction)
    {
        _nowDirection |= direction > 0 ? PlayerMoveDirection.Right : PlayerMoveDirection.Left;
        FlipPlayer();
    }

    /// <summary>
    /// 移動停止
    /// </summary>
    /// <param name="direction">移動方向</param>
    public void StopDirection(float direction)
    {
        _nowDirection &= direction > 0 ? ~PlayerMoveDirection.Right : ~PlayerMoveDirection.Left;
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
    /// 方向転換を無効・有効化する
    /// </summary>
    /// <param name="isLock">方向転換をさせるか</param>
    public void LockForword(bool isLock)
    {
        _isLockFlip = isLock;

        if(isLock)
        {
            var rot = transform.localEulerAngles;
            rot.y = 0;
            transform.localEulerAngles = rot;
        }
        else
        {
            FlipPlayer();
        }
    }

    /// <summary>
    /// 速度の数値を歩きとダッシュに分けて保管する構造体
    /// </summary>
    [Serializable]
    private struct WalkOrSprintProperty
    {
        /// <summary> 歩き状態での速度 </summary>
        [Tooltip("歩き状態での速度です。")]
        public float walk;
        /// <summary> ダッシュ状態での速度 </summary>
        [Tooltip("ダッシュ状態での速度です。")]
        public float sprintMultipiler;
    }
}
