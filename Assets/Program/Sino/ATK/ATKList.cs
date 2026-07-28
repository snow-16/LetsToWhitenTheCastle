using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ATKList : MonoBehaviour
{
    [Header("対応するプレファブを設定してください")]
    [SerializeField] GameObject _arrowPrefab;//矢のプレファブ
    [SerializeField] GameObject _gunPrefab;//弾のプレファブ
    [SerializeField] GameObject _fireDastPrefab;//火の粉のプレファブ
    [SerializeField] GameObject _canonBalletPrefab;//砲弾のプレファブ
    [SerializeField] GameObject _fallStonePrefab;//落石のプレファブ
    [Header("共通設定")]
    [Tooltip("生成されたオブジェクトの移動速度")]
    [SerializeField] float _moveSpeed = 6;
    [Header("矢の設定")]
    [Tooltip("一度に生成される矢の本数")]
    [SerializeField] int _createArrowSam = 3;
    [Tooltip("生成された矢どうしのy軸の距離")]
    [SerializeField] float _createArrowPosPlasY = 1;
    [Header("弾丸の設定")]
    [Tooltip("弾丸の移動速度にかかる_moveSpeed倍率")]
    [SerializeField] float _gunMoveSpeedMagnification = 1.5f;
    [Header("火の粉の設定")]
    [Tooltip("生成される火の粉の数")]
    [SerializeField] float _createFireDastSam = 6;
    [Tooltip("火の粉の移動速度にかかる_moveSpeed倍率")]
    [SerializeField] float _fireDastMoveSpeedMagnification = 0.7f;
    [Tooltip("火の粉が再度生成されるまでの時間")]
    [SerializeField] float _restCreateFireDastTime = 0.3f;
    [Header("砲弾の設定")]
    [Tooltip("着弾するまでの時間")]
    [SerializeField] float _hitPredictionTime = 2;
    [Header("落石の設定")]
    [Tooltip("プレイヤーのy軸上に生成される高さ")]
    [SerializeField] float _createStonePosYtoPlayer = 6;
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
        float createArrowPosY = 0;
        for (int i =0; i < _createArrowSam; i++)
        {
            GameObject arrow = Instantiate(_arrowPrefab, transform.position + Vector3.up * createArrowPosY, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = player.transform.position - arrow.transform.position;
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            BulletHitAndDestroySys Damage = arrow.GetComponent<BulletHitAndDestroySys>();
            Damage._damage = damage;
            rb.linearVelocity = direction.normalized * _moveSpeed;
            createArrowPosY += _createArrowPosPlasY;
            yield return null;
        }
        
    }

    public IEnumerator Gun(int damede)//bossの座標に弾を作成しプレイヤーに向かって飛ばす
    {
        GameObject gun = Instantiate(_gunPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - gun.transform.position;
        Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys Damage = gun.GetComponent<BulletHitAndDestroySys>();
        Damage._damage = damede;
        rb.linearVelocity = direction.normalized * _moveSpeed * _gunMoveSpeedMagnification;
        yield return null;
    }

    public IEnumerator FireDast(int damege)//bossの座標に火の粉を作成しプレイヤーに向かって飛ばす
    {
        for (int i = 0; i < _createFireDastSam; i++)
        {
            GameObject fireDast = Instantiate(_fireDastPrefab, transform.position, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = player.transform.position - fireDast.transform.position;
            Rigidbody2D rb = fireDast.GetComponent<Rigidbody2D>();
            BulletHitAndDestroySys Damage = fireDast.GetComponent<BulletHitAndDestroySys>();
            Damage._damage = damege;
            rb.linearVelocity = direction.normalized * _moveSpeed * 0.7f;
            fireDast.transform.localScale *= 2;
            yield return new WaitForSeconds(_restCreateFireDastTime);
        }
        //GameObject fireDast = Instantiate(_fireDastPrefab, transform.position, Quaternion.identity);
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Vector2 direction = player.transform.position - fireDast.transform.position;
        //Rigidbody2D rb = fireDast.GetComponent<Rigidbody2D>();
        //BulletHitAndDestroySys Damage = fireDast.GetComponent<BulletHitAndDestroySys>();
        //Damage._damage = damege;
        //rb.linearVelocity = direction.normalized * _moveSpeed * 0.7f;
        //fireDast.transform.localScale *= 2;
        //yield return null;
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

    public IEnumerator Cannon(int damage)//放物線で弾をplayerに飛ばす
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

    public IEnumerator FallStone(int damage)//プレイヤー上部に岩を生成し落とす
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
