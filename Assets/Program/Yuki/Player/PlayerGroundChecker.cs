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
    private LayerMask _groundLayer;
    /// <summary> 抜けられる床のレイヤー </summary>
    [SerializeField]
    private LayerMask _platformLayer;
    /// <summary> コヨーテタイムの長さ </summary>
    [SerializeField]
    private int _coyoteTime;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = transform.parent.GetComponent<PlayerStateHolder>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = 1 << collision.gameObject.layer;
        if((layer & _groundLayer) > 0 || (layer & _platformLayer) > 0)
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
                return (layer & _groundLayer) > 0 || (layer & _platformLayer) > 0;
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
