using System.Linq;
using UnityEngine;

/// <summary>
/// 手裏剣の移動用コンポーネント
/// </summary>
public class SyurikenMover : MonoBehaviour
{
    /// <summary> 軌跡の長さ </summary>
    [SerializeField]
    [Tooltip("手裏剣から引かれる軌跡の長さです。")]
    private float _orbitLength;

    /// <summary> 飛行速度 </summary>
    private float _moveSpeed;
    /// <summary> 飛行方向 </summary>
    private Vector2 _forword;
    /// <summary> 手裏剣が存在できる範囲 </summary>
    private Collider2D _surviveArea;

    /// <summary> LineRendererのインスタンス </summary>
    private LineRenderer _lineRenderer;

    public bool HitEnemy { get; set; }

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        transform.Translate(_forword * _moveSpeed, Space.World);
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position - (Vector3)_forword * _orbitLength);

        if(!Physics2D.OverlapCircleAll(transform.position, 0).Any(item => item == _surviveArea))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 投擲時の初期化処理
    /// </summary>
    /// <param name="speed">飛行速度</param>
    /// <param name="forword">飛行方向</param>
    public void Throw(float speed, Vector2 forword, Collider2D surviveArea)
    {
        _moveSpeed = speed;
        _forword = forword;
        _surviveArea = surviveArea;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player" && collision.TryGetComponent<LifeSystem>(out var target))
        {
            GetComponent<AttackDamager>().Attack(target);
            HitEnemy = true;
        }
    }
}
