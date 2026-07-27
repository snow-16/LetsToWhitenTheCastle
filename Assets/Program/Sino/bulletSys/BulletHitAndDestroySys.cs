using UnityEngine;

public class BulletHitAndDestroySys : MonoBehaviour
{
    public int _damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))//playerならダメージを与える
        {
            GameObject player = collision.gameObject;
            LifeSystem playerLifeSys = player.GetComponent<LifeSystem>();
            playerLifeSys.FluctuationHP(_damage);
            Debug.Log(_damage + "ダメージ与えました");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))//壁や地面なら消滅する
        {
            Destroy(gameObject);
        }
    }

}
