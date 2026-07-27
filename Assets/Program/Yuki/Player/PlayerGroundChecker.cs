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

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = GetComponent<PlayerStateHolder>();
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
}
