using System.Linq;
using UnityEngine;

/// <summary>
/// 手裏剣の移動用コンポーネント
/// </summary>
public class SyurikenMover : MonoBehaviour
{
    /// <summary> 飛行速度 </summary>
    private float _moveSpeed;
    /// <summary> 飛行方向 </summary>
    private Vector2 _forword;
    /// <summary> 手裏剣が存在できる範囲 </summary>
    private Collider2D _surviveArea;

    void FixedUpdate()
    {
        transform.Translate(_forword * _moveSpeed, Space.World);

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
}
