using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossAI : MonoBehaviour
{
    [Tooltip("攻撃のクールダウン")]
    [SerializeField] float _attackCoolDownTime; //攻撃のクールダウン
    bool _isCoolDown = false;//クールダウンが発生しているか否か
    public bool _isActing = false;
    [Tooltip("距離によって攻撃方法を変えるか")]
    public bool _attackChangeLange = true;//距離によって攻撃方法を変えるか
    [Tooltip("残り体力によって攻撃方法を変更するかどうか")]
    public bool _attackChangeHP = false;//残り体力によって攻撃方法を変更するかどうか
    [Tooltip("小攻撃を何回行うと中攻撃を行うか")]
    [SerializeField] int _attackCount1To2 = 3;//小攻撃を何回打つと中攻撃を行うか
    int _nowAttackCount1To2;//今何回目の小攻撃か
    [Tooltip("中攻撃を何回行うと大攻撃を行うか")]
    [SerializeField] int _attackCount2To3 = 0;//中攻撃を何回行うと大攻撃を行うか
    int _nowAttackCount2To3;//今何度目の中攻撃か
    [Header("攻撃時の処理")]
    [Tooltip("小攻撃の処理")]
    [SerializeField] UnityEvent _attackEventLange1 = null;
    [Tooltip("中攻撃の処理")]
    [SerializeField] UnityEvent _attackEventLange2 = null;
    [Tooltip("大攻撃の処理")]
    [SerializeField] UnityEvent _attackEventLange3 = null;
    [Header("HPによる攻撃時の処理")]
    [Tooltip("小攻撃")]
    [SerializeField] UnityEvent _attackEventHP1 = null;
    [Tooltip("中攻撃")]
    [SerializeField] UnityEvent _attackEventHP2 = null;
    [Tooltip("大攻撃")]
    [SerializeField] UnityEvent _attackEventHP3 = null;
    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(StartAttack());
    }

    IEnumerator StartHpAttack()
    {
        yield return null;
    }

    IEnumerator StartAttack()
    {
        if (_nowAttackCount2To3 == _attackCount2To3)
        {
            _nowAttackCount2To3 = 0;
            _attackEventLange3.Invoke();
            yield return new WaitUntil(() => _isActing);
            Debug.Log("3攻撃");
            _isCoolDown = true;
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
        }
        else if (_nowAttackCount1To2 == _attackCount1To2)
        {
            _nowAttackCount1To2 = 0;
            _attackEventLange2.Invoke();
            yield return new WaitUntil(() => _isActing);
            Debug.Log("2攻撃");
            _isCoolDown = true;
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount2To3++;
        }
        else if (_nowAttackCount1To2 != _attackCount1To2)
        {
            _attackEventLange1.Invoke();
            yield return new WaitUntil(() => _isActing);
            Debug.Log("1攻撃");
            _isCoolDown = true;
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount1To2++;
        }

    }
}
