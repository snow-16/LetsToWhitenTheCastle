using System.Linq;
using UnityEngine;

/// <summary>
/// 爆弾の移動用コンポーネント
/// </summary>
public class BombMover : MonoBehaviour
{
    /// <summary> 爆発範囲指定コライダー </summary>
    [SerializeField]
    [Tooltip("爆発範囲を示すコライダーです。")]
    private CircleCollider2D _bombRangeCollider;

    /// <summary> 放物線の高さ </summary>
    private float _throwHeight;
    /// <summary> 飛行速度 </summary>
    private float _moveSpeed;
    /// <summary> 投擲地点 </summary>
    private Vector2 _basePoint;
    /// <summary> 着弾地点 </summary>
    private Vector2 _targetPoint;

    /// <summary> 移動経過率 </summary>
    private float _progress;
    /// <summary> 爆発範囲 </summary>
    private float _bombRange;

    void Start()
    {
        _bombRange = _bombRangeCollider.radius;
        Destroy(_bombRangeCollider);
    }

    void FixedUpdate()
    {
        var pos = Vector2.Lerp(_basePoint, _targetPoint, _progress);
        pos.y += Mathf.Sin(_progress * Mathf.PI) * (_targetPoint - _basePoint).magnitude * _throwHeight;
        transform.position = pos;
        _progress = Mathf.Min(_progress + _moveSpeed / (_targetPoint - _basePoint).magnitude, 1);

        if(_progress == 1)
        {
            var boss = Physics2D.OverlapCircleAll(transform.position, _bombRange).First(hit => hit.tag != "Player");

            if(boss.TryGetComponent<LifeSystem>(out var bossLife))
            {
                GetComponent<AttackDamager>().Attack(bossLife);
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 投擲時の初期化処理
    /// </summary>
    /// <param name="speed">飛行速度</param>
    /// <param name="forword">飛行方向</param>
    public void Throw(float speed, Vector2 targetPoint, float height)
    {
        _moveSpeed = speed;
        _basePoint = transform.position;
        _targetPoint = targetPoint;
        _throwHeight = height;
    }
}
