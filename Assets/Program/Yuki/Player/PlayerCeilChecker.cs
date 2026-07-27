using UnityEngine;

/// <summary>
/// プレイヤーが天井に触れたかを判定するコンポーネント
/// </summary>
public class PlayerCeilChecker : MonoBehaviour
{
    /// <summary> 判定用コライダー </summary>
    [SerializeField]
    private Collider2D _groundHitCircle;
    /// <summary> 天井のレイヤー </summary>
    [SerializeField]
    private LayerMask _groundLayer;

    /// <summary> PlayerStateHolderのインスタンス </summary>
    private PlayerStateHolder _playerStateHolder;

    void Start()
    {
        _playerStateHolder = transform.parent.GetComponent<PlayerStateHolder>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = 1 << collision.gameObject.layer;
        if((layer & _groundLayer) > 0)
        {
            if(_playerStateHolder.PlayerJumpState == PlayerJumpState.Rise)
            {
                
                _playerStateHolder.PlayerJumpState = PlayerJumpState.Fall;
            }
        }
    }
}
