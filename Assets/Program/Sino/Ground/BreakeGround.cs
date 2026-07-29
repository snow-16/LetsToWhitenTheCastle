using UnityEngine;
using UnityEngine.Events;

public class BreakeGround : MonoBehaviour
{
    [Tooltip("砲弾が当たったときの処理")]
    [SerializeField]UnityEvent _whenCollisonCanonBullet = null;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanonBullet"))
        {
            _whenCollisonCanonBullet?.Invoke();
        }
    }
}
