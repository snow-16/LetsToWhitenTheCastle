using UnityEngine;

/// <summary>
/// プレイヤーのジャンプ移動用コンポーネント
/// </summary>
public class PlayerJumper : MonoBehaviour
{
    /// <summary> 各移動力の大きさの倍率。大きいほど細かく動く </summary>
    [SerializeField]
    [Tooltip("動きの細やかさです。速度などを0.05〜などで設定しなくて良くします。")]
    private float _movingScale;
    /// <summary> ジャンプの初速 </summary>
    [SerializeField]
    [Tooltip("ジャンプの初速です。")]
    private float _initialJumpPower;
    /// <summary> ジャンプの伸び率 </summary>
    [SerializeField]
    [Tooltip("長押しでの上昇力です。")]
    private float _keepJumpPower;
    /// <summary> ジャンプの最大距離 </summary>
    [SerializeField]
    [Tooltip("ジャンプの最大飛距離です。")]
    private float _maxJump;

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
                    _rigidbody2D.linearVelocityY = 0;
                    _nowJumpPower += _keepJumpPower / _movingScale;
                }

                transform.Translate(0, _nowJumpPower, 0);

                if(transform.position.y >= _groundHeight + _maxJump || _rigidbody2D.linearVelocityY < 0)
                {
                    _playerStateHolder.PlayerJumpState = PlayerJumpState.Fall;
                    _rigidbody2D.linearVelocityY = 0;
                }
            }
            else if(_isStrain)
            {
                _isStrain = false;
            }
        }
    }

    /// <summary>
    /// 跳躍する
    /// </summary>
    public void StartJump()
    {
        _rigidbody2D.linearVelocityY = 0;
        _groundHeight = transform.position.y;
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
