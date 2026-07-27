using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossAI : MonoBehaviour
{
    Animator _anim;//アニメーション
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
    [Tooltip("小攻撃のアニメーションの名前")]
    public string _attackEvent1AnimState1;//小攻撃時のアニメーションのステート名
    [Tooltip("小攻撃の処理")]
    [SerializeField] UnityEvent _attackEvent1 = null;//小攻撃時の処理
    [Tooltip("中攻撃のアニメーションの名前")]
    public string _attackEvent1AnimState2;//中攻撃のアニメーションのステート名
    [Tooltip("中攻撃の処理")]
    [SerializeField] UnityEvent _attackEvent2 = null;//中攻撃の処理
    [Tooltip("大攻撃のアニメーションの名前")]
    public string _attackEvent1AnimState3;//大攻撃のアニメーションステート名
    [Tooltip("大攻撃の処理")]
    [SerializeField] UnityEvent _attackEvent3 = null;//大攻撃の処理
    [Header("HPによる攻撃時の処理")]
    [Tooltip("小攻撃")]
    [SerializeField] UnityEvent _attackEventHP1 = null;
    [Tooltip("中攻撃")]
    [SerializeField] UnityEvent _attackEventHP2 = null;
    [Tooltip("大攻撃")]
    [SerializeField] UnityEvent _attackEventHP3 = null;
    void Start()
    {
        _anim = GetComponent<Animator>();
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
        float animTime;
        if (_nowAttackCount2To3 == _attackCount2To3)
        {
            _nowAttackCount2To3 = 0;
            _anim.Play(_attackEvent1AnimState3);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEvent3.Invoke();
            Debug.Log("3攻撃");
            _isCoolDown = true;
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
        }
        else if (_nowAttackCount1To2 == _attackCount1To2)
        {
            _nowAttackCount1To2 = 0;
            _anim.Play(_attackEvent1AnimState2);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEvent2.Invoke();
            Debug.Log("2攻撃");
            _isCoolDown = true;
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount2To3++;
        }
        else if (_nowAttackCount1To2 != _attackCount1To2)
        {
            _attackEvent1.Invoke();
            _anim.Play(_attackEvent1AnimState1);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            Debug.Log("1攻撃");
            _isCoolDown = true;
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount1To2++;
        }

    }
}
