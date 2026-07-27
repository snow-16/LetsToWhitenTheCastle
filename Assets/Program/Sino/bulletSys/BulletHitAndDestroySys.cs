using UnityEngine;

public class BulletHitAndDestroySys : MonoBehaviour
{
    public int _damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            LifeSystem playerLifeSys = player.GetComponent<LifeSystem>();
            playerLifeSys.FluctuationHP(_damage);
            Destroy(gameObject);
        }
    }

}
