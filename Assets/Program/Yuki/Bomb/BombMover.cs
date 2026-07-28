using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// 爆弾の移動用コンポーネント
/// </summary>
public class BombMover : MonoBehaviour
{
    /// <summary> 障害物のレイヤー </summary>
    [SerializeField]
    [Tooltip("着弾可能なレイヤーです。複数設定できます。")]
    private LayerMask _hitableLayer;
    /// <summary> 爆発範囲指定コライダー </summary>
    [SerializeField]
    [Tooltip("爆発範囲を示すコライダーです。")]
    private CircleCollider2D _bombRangeCollider;

    /// <summary> 移動速度 </summary>
    private float _speed;
    /// <summary> 移動経過率 </summary>
    private float _progress;
    /// <summary> 爆発範囲 </summary>
    private float _bombRange;
    /// <summary> 投擲軌道の計算関数 </summary>
    private Func<float, Vector2> _throwFunc;

    void Start()
    {
        _bombRange = _bombRangeCollider.radius;
        Destroy(_bombRangeCollider);
    }

    void FixedUpdate()
    {
        transform.position = (Vector3)_throwFunc((_progress += Time.deltaTime) * _speed);

        if(transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 投擲時の初期化処理
    /// </summary>
    /// <param name="speed">飛行速度</param>
    /// <param name="forword">飛行方向</param>
    public void Throw(Func<float, Vector2> throwFunc, float speed)
    {
        _throwFunc = throwFunc;
        _speed = speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && ((1 << collision.gameObject.layer) & _hitableLayer) > 0)
        {
            var boss = Physics2D.OverlapCircleAll(transform.position, _bombRange);

            if(boss.Length > 0 && boss.First(hit => hit.tag != "Player").TryGetComponent<LifeSystem>(out var bossLife))
            {
                GetComponent<AttackDamager>().Attack(bossLife);
            }

            Destroy(gameObject);
        }
    }
}
