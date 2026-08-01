using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ移動用コンポーネント
/// </summary>
public class PlayerJumper : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private PlayerJumpdata _jumpData;
    /// <summary> 各移動力の大きさの倍率。大きいほど細かく動く </summary>
    [SerializeField]
    [Tooltip("動きの細やかさです。速度などを0.05〜などで設定しなくて良くします。")]
    private float _movingScale;

    /// <summary> ジャンプ時の地面の高さ </summary>
    private float _groundHeight;
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
                    _rigidbody2D.linearVelocityY = -0.01f;
                    _nowJumpPower += _jumpData.KeepJumpPower / _movingScale;
                }

                transform.Translate(0, _nowJumpPower, -0.01f);

                if(transform.position.y >= _groundHeight + _jumpData.MaxJump || _rigidbody2D.linearVelocityY < -0.01f)
                {
                    _playerStateHolder.PlayerJumpState = PlayerJumpState.Fall;
                    _rigidbody2D.linearVelocityY = -0.01f;
                }
            }
            else if(_isStrain)
            {
                _isStrain = false;
            }
            else if(_playerStateHolder.PlayerJumpState == PlayerJumpState.Fall && _rigidbody2D.linearVelocityY == 0)
            {
                _playerStateHolder.PlayerJumpState = PlayerJumpState.OnGround;
            }
        }
    }

    /// <summary>
    /// 跳躍する
    /// </summary>
    public void StartJump()
    {
        if(_playerStateHolder.JumpCount < _jumpData.JumpableCount)
        {
            _rigidbody2D.linearVelocityY = 0;
            _groundHeight = transform.position.y;
            _nowJumpPower = _jumpData.InitialJumpPower / _movingScale;
            _isStrain = true;
            _playerStateHolder.PlayerJumpState = PlayerJumpState.Rise;
            _playerStateHolder.JumpCount++;
            SoundSystem.Instance.PlaySE(SEType.Jump);
        }
    }

    /// <summary>
    /// 力を抜いて落下する
    /// </summary>
    public void EndJump()
    {
        _isStrain = false;
    }
}
