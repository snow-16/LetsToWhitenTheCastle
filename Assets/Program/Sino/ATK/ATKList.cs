using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ATKList : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] GameObject _gunPrefab;
    [SerializeField] GameObject _fireDastPrefab;
    [SerializeField] GameObject _canonBalletPrefab;
    [SerializeField] GameObject _fallStonePrefab;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _hitPredictionTime;
    [SerializeField] float _createStonePosYtoPlayer;
    int _movementAttackDamge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Arrow(int damage)//bossの座標に矢を作成しプレイヤーに向かって飛ばす
    {
        GameObject arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - arrow.transform.position;
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys Damage = arrow.GetComponent<BulletHitAndDestroySys>();
        Damage._damage = damage;
        rb.linearVelocity = direction.normalized * _moveSpeed;
        yield return null;
    }

    public IEnumerator Gun(int damede)//bossの座標に弾を作成しプレイヤーに向かって飛ばす
    {
        GameObject gun = Instantiate(_gunPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - gun.transform.position;
        Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys Damage = gun.GetComponent<BulletHitAndDestroySys>();
        Damage._damage = damede;
        rb.linearVelocity = direction.normalized * _moveSpeed * 1.5f;
        yield return null;
    }

    public IEnumerator FireDast(int damege)//bossの座標に火の粉を作成しプレイヤーに向かって飛ばす
    {
        GameObject fireDast = Instantiate(_fireDastPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - fireDast.transform.position;
        Rigidbody2D rb = fireDast.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys Damage = fireDast.GetComponent<BulletHitAndDestroySys>();
        Damage._damage = damege;
        rb.linearVelocity = direction.normalized * _moveSpeed * 0.7f;
        fireDast.transform.localScale *= 2;
        yield return null;
    }

    public void RandamLitteleATKSelect(int damage)//上記の攻撃をランダムに行う
    {
        _movementAttackDamge = damage;
        int AttackNum = Random.Range(0, 3);
        if (AttackNum == 0)
        {
            StartCoroutine(Arrow(damage));
        }
        else if (AttackNum == 1)
        {
            StartCoroutine(Gun(damage));
        }
        else
        {
            StartCoroutine(FireDast(damage));
        }
    }

    private Vector2 CalcuateArcVelocity2D(Rigidbody2D rb, Vector2 distance, float time)
    {
        float velosityX = distance.x / time;
        float gravity = Mathf.Abs(Physics2D.gravity.y) * rb.gravityScale;
        float velosityY = (distance.y / time) + (0.5f * gravity * time);
        return new Vector2(velosityX, velosityY);
    }

    public IEnumerator Cannon(int damage)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 distans = player.transform.position - transform.position;
        GameObject canonBullet = Instantiate(_canonBalletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = canonBullet.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys canonDamage = canonBullet.GetComponent<BulletHitAndDestroySys>();
        canonDamage._damage = damage;
        rb.linearVelocity = CalcuateArcVelocity2D(rb, distans, _hitPredictionTime);
        yield return null;
    }

    public IEnumerator FallStone(int damage)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject fallStone = Instantiate(_fallStonePrefab, player.transform.position + Vector3.up * _createStonePosYtoPlayer, Quaternion.identity);
        BulletHitAndDestroySys fallStoneDmage = fallStone.GetComponent<BulletHitAndDestroySys>();
        fallStoneDmage._damage = damage;
        yield return null;
    }

    public void RandamMediumATKSelect(int damage)//上記二つの攻撃をランダムに行う
    {
        int selectAttack = Random.Range(0, 2);
        if (selectAttack == 0) StartCoroutine(Cannon(damage));
        else if(selectAttack == 1) StartCoroutine(FallStone(damage));

    }
}
