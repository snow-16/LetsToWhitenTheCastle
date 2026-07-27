using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ATKList : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] GameObject _gunPrefab;
    [SerializeField] GameObject _firePrefab;
    [SerializeField] GameObject _canonBalletPrefab;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _hitPredictionTime;
    int _movementAttackDamge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Arrow()//bossの座標に矢を作成しプレイヤーに向かって飛ばす
    {
        GameObject arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - arrow.transform.position;
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys damageSys = arrow.GetComponent<BulletHitAndDestroySys>();
        rb.linearVelocity = direction * _moveSpeed;
        yield return null;
    }

    public IEnumerator Gun()//bossの座標に弾を作成しプレイヤーに向かって飛ばす
    {
        GameObject gun = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - gun.transform.position;
        Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys damageSys = gun.GetComponent<BulletHitAndDestroySys>();
        rb.linearVelocity = direction * _moveSpeed * 1.5f;
        yield return null;
    }

    public IEnumerator FireDast()//bossの座標に火の粉を作成しプレイヤーに向かって飛ばす
    {
        GameObject fireDast = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - fireDast.transform.position;
        Rigidbody2D rb = fireDast.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys damageSys = fireDast.GetComponent<BulletHitAndDestroySys>();
        rb.linearVelocity = direction * _moveSpeed * 0.8f;
        fireDast.transform.localScale *= 2;
        yield return null;
    }

    public void RandamLTLATK(int damage)
    {
        _movementAttackDamge = damage;
        int AttackNum = Random.Range(0, 3);
        if (AttackNum == 0)
        {
            StartCoroutine("Arrow");
        }
        else if (AttackNum == 1)
        {
            StartCoroutine("Gun");
        }
        else
        {
            StartCoroutine("FireDast");
        }
    }

    private Vector2 CalcuateArcVelocity2D(Rigidbody2D rb, Vector2 distance, float time)
    {
        float velosityX = distance.x / time;
        float gravity = Mathf.Abs(Physics2D.gravity.y) * rb.gravityScale;
        float velosityY = (distance.y / time) + (0.5f * gravity * time);
        return new Vector2(velosityX, velosityY);
    }

    public IEnumerator Cannon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 distans = player.transform.position - transform.position;
        GameObject canonBullet = Instantiate(_canonBalletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = canonBullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = CalcuateArcVelocity2D(rb, distans, _hitPredictionTime);
        yield return null;
    }

    public void MDLATK()
    {
        StartCoroutine(Cannon());
    }
}
