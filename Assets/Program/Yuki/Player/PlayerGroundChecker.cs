using System.Linq;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーが地面にいるかを判定するコンポーネント
/// </summary>
public class PlayerGroundChecker : MonoBehaviour
{
    /// <summary> 判定用コライダー </summary>
    [SerializeField]
    private Collider2D _groundHitCircle;
    /// <summary> 地面のレイヤー </summary>
    [SerializeField]
    [Tooltip("地面と認識するレイヤーです。複数設定できます。")]
    private LayerMask _groundLayer;
    /// <summary> 抜けられる床のレイヤー </summary>
    [SerializeField]
    [Tooltip("すり抜けられる床と認識するレイヤーです。複数設定できます。")]
    private LayerMask _platformLayer;
    /// <summary> コヨーテタイムの長さ </summary>
    [SerializeField]
    [Tooltip("床の判定が消えてからジャンプできなくなるまでの猶予時間です。")]
    private int _coyoteTime;

    /// <summary> すり抜け床をすり抜けるか </summary>
    private bool _skipPlatform = false;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = transform.parent.GetComponent<PlayerStateHolder>();
    }

    /// <summary>
    /// 床のすり抜け状態を更新する
    /// </summary>
    /// <param name="skip">すり抜けるか</param>
    public void SwitchSkipPlatform(bool skip)
    {
        _skipPlatform = skip;

        transform.parent.GetComponent<Rigidbody2D>().excludeLayers = skip ? _platformLayer : 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = 1 << collision.gameObject.layer;
        if((layer & _groundLayer) > 0 || (!_skipPlatform && (layer & _platformLayer) > 0))
        {
            if(_playerStateHolder.PlayerJumpState == PlayerJumpState.Fall)
            {
                _playerStateHolder.PlayerJumpState = PlayerJumpState.OnGround;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var existGroundsCount = Physics2D.OverlapCircleAll(transform.position, ((CircleCollider2D)_groundHitCircle).radius)
        .Where(col =>
            {
                var layer = 1 << col.gameObject.layer;
                return (layer & _groundLayer) > 0 || (!_skipPlatform && (layer & _platformLayer) > 0);
            }
        ).ToArray().Length;

        if(existGroundsCount == 0 && _playerStateHolder.PlayerJumpState == PlayerJumpState.OnGround)
        {
            Observable.Timer(new System.TimeSpan(0, 0, 0, 0, _coyoteTime)).TakeUntil(Observable.EveryUpdate().Where(_ => _playerStateHolder.PlayerJumpState != PlayerJumpState.OnGround)).Subscribe(_ =>
                {
                    _playerStateHolder.PlayerJumpState = PlayerJumpState.Fall;
                }
            ).AddTo(this);
        }
    }
}
