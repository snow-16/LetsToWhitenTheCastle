using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ移動用コンポーネント
/// </summary>
public class PlayerJumper : MonoBehaviour
{
    /// <summary> 各移動力の大きさの倍率。大きいほど細かく動く </summary>
    [SerializeField]
    private float _movingScale;
    /// <summary> ジャンプの初速 </summary>
    [SerializeField]
    private float _initialJumpPower;
    /// <summary> ジャンプの伸び率 </summary>
    [SerializeField]
    private float _keepJumpPower;
    /// <summary> ジャンプの最大距離 </summary>
    [SerializeField]
    private float _maxJump;

    /// <summary> 現在の跳躍力 </summary>
    private float _nowJumpPower;
    /// <summary> 飛距離を伸ばすかどうか </summary>
    private bool _isStrain;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;
    /// <summary> Rigidbody2Dのインスタンス </summary>
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _playerStateHolder = GetComponent<PlayerStateHolder>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(_playerStateHolder.PlayerJumpState != PlayerJumpState.OnGround)
        {
            if(_playerStateHolder.PlayerJumpState == PlayerJumpState.Rise)
            {
                if(_isStrain)
                {
                    _nowJumpPower = Mathf.Min(_nowJumpPower + _keepJumpPower / _movingScale, _maxJump);
                }

                transform.Translate(0, _nowJumpPower, 0);

                if(Mathf.Abs(_rigidbody2D.linearVelocityY) / _rigidbody2D.gravityScale > _nowJumpPower)
                {
                    _isStrain = false;
                    _playerStateHolder.PlayerJumpState = PlayerJumpState.Fall;
                    _rigidbody2D.linearVelocityY = 0;
                }
            }
        }
    }

    /// <summary>
    /// 跳躍する
    /// </summary>
    public void StartJump()
    {
        _rigidbody2D.linearVelocityY = 0;
        _nowJumpPower = _initialJumpPower / _movingScale;
        _isStrain = true;
        _playerStateHolder.PlayerJumpState = PlayerJumpState.Rise;
    }

    /// <summary>
    /// 力を抜いて落下する
    /// </summary>
    public void EndJump()
    {
        _isStrain = false;
    }
}
