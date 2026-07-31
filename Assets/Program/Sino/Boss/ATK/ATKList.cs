using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ATKList : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    [Header("対応するプレファブを設定してください")]
    [SerializeField] GameObject _arrowPrefab;//矢のプレファブ
    [SerializeField] GameObject _gunPrefab;//弾のプレファブ
    [SerializeField] GameObject _fireDastPrefab;//火の粉のプレファブ
    [SerializeField] GameObject _canonBalletPrefab;//砲弾のプレファブ
    [SerializeField] GameObject[] _fallStonePrefab;//落石のプレファブ
    [SerializeField] GameObject _slashPrefab;//斬撃のプレファブ

    [Header("共通設定")]
    [Tooltip("生成されたオブジェクトの移動速度")]
    [SerializeField] float _moveSpeed = 8;

    [Header("矢の設定")]
    [Tooltip("一度に生成される矢の本数")]
    [SerializeField] int _createArrowSam = 3;
    [Tooltip("生成された矢どうしのy軸の距離")]
    [SerializeField] float _createArrowPosPlasY = 1;
    [Tooltip("攻撃時に起きるイベント")]
    [SerializeField] UnityEvent _arrowEvent = null;

    [Header("弾丸の設定")]
    [Tooltip("弾丸の移動速度にかかる_moveSpeed倍率")]
    [SerializeField] float _gunMoveSpeedMagnification = 3;
    [Tooltip("狙われたときの色")]
    [SerializeField] private Color _flashColor = Color.red;
    [Tooltip("色が変わっている時間（秒）")]
    [SerializeField] private float _flashDuration = 0.05f;
    [Tooltip("攻撃時に起きるイベント")]
    [SerializeField] UnityEvent _gunEvent = null;

    [Header("火の粉の設定")]
    [Tooltip("生成される火の粉の数")]
    [SerializeField] float _createFireDastSam = 6;
    [Tooltip("火の粉の移動速度にかかる_moveSpeed倍率")]
    [SerializeField] float _fireDastMoveSpeedMagnification = 0.7f;
    [Tooltip("火の粉が再度生成されるまでの時間")]
    [SerializeField] float _restCreateFireDastTime = 0.2f;
    [Tooltip("攻撃時に起きるイベント")]
    [SerializeField] UnityEvent _fireDastEvent = null;

    [Header("砲弾の設定")]
    [Tooltip("生成される砲弾の数")]
    [SerializeField] float _createCanonBulletSam = 6;
    [Tooltip("着弾するまでの時間")]
    [SerializeField] float _hitPredictionTime = 2;
    [Tooltip("砲弾が再度生成されるまでの時間")]
    [SerializeField] float _restCreateCanonBulletTime = 1;
    [Tooltip("攻撃時に起きるイベント")]
    [SerializeField] UnityEvent _canonEvent = null;

    [Header("落石の設定")]
    [Tooltip("生成される落石の数")]
    [SerializeField] float _createStoneSam = 6;
    [Tooltip("落石が生成されるX軸の幅")]
    [SerializeField] float _CreateStoneRandamPosx = 3;
    [Tooltip("プレイヤーのy軸上に生成される高さ")]
    [SerializeField] float _createStonePosYtoPlayer = 5;
    [Tooltip("落石が再度生成されるまでの時間")]
    [SerializeField] float _restCreateStoneTime = 1;
    [Tooltip("攻撃時に起きるイベント")]
    [SerializeField] UnityEvent _fallStoneEvent = null;

    [Header("斬撃の設定")]
    [Tooltip("生成される斬撃の数")]
    [SerializeField] float _createSlashSam = 6;
    [Tooltip("斬撃の移動速度にかかる_moveSpeed倍率")]
    [SerializeField] float _SlashMoveSpeedMagnification = 1.3f;
    [Tooltip("斬撃が再度生成されるまでの時間")]
    [SerializeField] float _restCreateSlashTime = 0.5f;
    [Tooltip("攻撃時に起きるイベント")]
    [SerializeField] UnityEvent _slashEvent = null;
    int _movementAttackDamge;

    public IEnumerator Arrow(int damage)//bossの座標に矢を作成しプレイヤーに向かって飛ばす
    {
        float createArrowPosY = 0;
        for (int i = 0; i < _createArrowSam; i++)
        {
            _arrowEvent?.Invoke();
            Debug.Log("ArrowAttack");
            GameObject arrow = Instantiate(_arrowPrefab, transform.position + Vector3.up * createArrowPosY, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = player.transform.position - arrow.transform.position;
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            BulletHitAndDestroySys Damage = arrow.GetComponent<BulletHitAndDestroySys>();
            Damage._damage = damage;
            Vector2 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
            rb.linearVelocity = direction.normalized * _moveSpeed;
            createArrowPosY += _createArrowPosPlasY;
            yield return null;
        }

    }

    public IEnumerator Gun(int damede)//bossの座標に弾を作成しプレイヤーに向かって飛ばす
    {
        Debug.Log("GunAttack");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _spriteRenderer = player.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            _originalColor = _spriteRenderer.color;
            _spriteRenderer.color = _flashColor;
            yield return new WaitForSeconds(_flashDuration);
            _spriteRenderer.color = _originalColor;
            yield return new WaitForSeconds(_flashDuration);
        }
        _gunEvent?.Invoke();
        GameObject gun = Instantiate(_gunPrefab, transform.position, Quaternion.identity);
        Vector2 direction = player.transform.position - gun.transform.position;
        Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
        BulletHitAndDestroySys Damage = gun.GetComponent<BulletHitAndDestroySys>();
        Damage._damage = damede;
        rb.linearVelocity = direction.normalized * _moveSpeed * _gunMoveSpeedMagnification;
    }

    public IEnumerator FireDast(int damege)//bossの座標に火の粉を作成しプレイヤーに向かって飛ばす
    {
        _fireDastEvent?.Invoke();
        for (int i = 0; i < _createFireDastSam; i++)
        {
            Debug.Log("FireDastAttack");
            GameObject fireDast = Instantiate(_fireDastPrefab, transform.position, Quaternion.identity);
            SpriteRenderer sp = fireDast.GetComponent<SpriteRenderer>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = player.transform.position - fireDast.transform.position;
            Rigidbody2D rb = fireDast.GetComponent<Rigidbody2D>();
            BulletHitAndDestroySys Damage = fireDast.GetComponent<BulletHitAndDestroySys>();
            Damage._damage = damege;
            rb.linearVelocity = direction.normalized * _moveSpeed * _fireDastMoveSpeedMagnification;
            fireDast.transform.localScale *= 2;
            sp.sortingOrder = 0 - i;

            yield return new WaitForSeconds(_restCreateFireDastTime);
        }
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
        Debug.Log("CannonAttack");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        for(int i = 0; i <= _createCanonBulletSam; i++)
        {
            _canonEvent?.Invoke();
            Vector2 distans = player.transform.position - transform.position;
            GameObject canonBullet = Instantiate(_canonBalletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = canonBullet.GetComponent<Rigidbody2D>();
            BulletHitAndDestroySys canonDamage = canonBullet.GetComponent<BulletHitAndDestroySys>();
            canonDamage._damage = damage;
            rb.linearVelocity = CalcuateArcVelocity2D(rb, distans, _hitPredictionTime);
            yield return new WaitForSeconds(_restCreateCanonBulletTime);
        }
        
    }

    public IEnumerator FallStone(int damage)//プレイヤー上部に岩を生成し落とす
    {
        Debug.Log("FallStoneAttack");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; _createStoneSam > i; i++)
        {
            _fallStoneEvent?.Invoke();
            int randamFallStoneNam = Random.Range(0, _fallStonePrefab.Length);
            float posx = player.transform.position.x + Random.Range(-_CreateStoneRandamPosx, _CreateStoneRandamPosx);
            float posy = player.transform.position.y + _createStonePosYtoPlayer;
            Vector3 stonePos = new Vector3(posx, posy, player.transform.position.z);
            GameObject fallStone = Instantiate(_fallStonePrefab[randamFallStoneNam], stonePos, Quaternion.identity);
            BulletHitAndDestroySys fallStoneDamage = fallStone.GetComponent<BulletHitAndDestroySys>();
            fallStoneDamage._damage = damage;
            yield return new WaitForSeconds(_restCreateStoneTime);
        }
    }

    public void RandamMediumATKSelect(int damage)//上記二つの攻撃をランダムに行う
    {
        int selectAttack = Random.Range(0, 2);
        if (selectAttack == 0) StartCoroutine(Cannon(damage));
        else if (selectAttack == 1) StartCoroutine(FallStone(damage));

    }

    public IEnumerator Slash(int damege)
    {
        for (int i = 0; i < _createSlashSam; i++)
        {
            _slashEvent?.Invoke();
            GameObject slash = Instantiate(_slashPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = slash.GetComponent<Rigidbody2D>();
            BulletHitAndDestroySys slashDamage = slash.GetComponent<BulletHitAndDestroySys>();
            rb.linearVelocityX = -_moveSpeed * _SlashMoveSpeedMagnification;
            slashDamage._damage = damege;
            yield return new WaitForSeconds(_restCreateSlashTime);
        }
    }

    public void UltATK(int damage)
    {
        StartCoroutine(Slash(damage));
    }
}
