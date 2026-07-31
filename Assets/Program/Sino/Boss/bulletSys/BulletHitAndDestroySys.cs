using UnityEngine;

public class BulletHitAndDestroySys : MonoBehaviour
{
    public int _damage;
    public bool _isParry = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isParry)
        {
            if (collision.gameObject.CompareTag("Boss"))//Bossならダメージを与える
            {
                _damage = 1;
                GameObject boss = collision.gameObject;
                LifeSystem bossLifeSys = boss.GetComponent<LifeSystem>();
                bossLifeSys.FluctuationHP(_damage);
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))//壁や地面なら消滅する
            {
                Destroy(gameObject);
            }
        }
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
