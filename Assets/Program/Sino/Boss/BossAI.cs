using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossAI : MonoBehaviour
{
    Animator _anim;//アニメーション
    [Tooltip("カメラのアニメーター")]
    [SerializeField]Animator _cameraAnim;//カメラのアニメーション
    LifeSystem _lifeSystem;//ボスのライフシステム
    [Header("最初の咆哮")]
    [Tooltip("最初のアニメーションのステート名")]
    [SerializeField] string _startAnimName;
    [Tooltip("アニメーション以外で開始時に行うイベント")]
    [SerializeField] UnityEvent _startEvent = null;
    public bool _isStartAnim = true;
    [Header("共通設定")]
    [Tooltip("攻撃のクールダウン")]
    [SerializeField] float _attackCoolDownTime; //攻撃のクールダウン
    [Tooltip("第二形態時の攻撃のクールダウン")]
    [SerializeField] float _LowHPattacCoolDownTime; //攻撃のクールダウン
    bool _isCoolDown = false;//クールダウンが発生しているか否か
    [Tooltip("待機時のアニメーションのステート名")]
    [SerializeField] string _restAnimName;//_restAnimName
    [Tooltip("アニメーション以外で休憩時に行うイベント")]
    [SerializeField] UnityEvent _restEvent = null;//アニメーション以外で休憩時に行うイベント
    [Header("AI設定")]
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
    [SerializeField] float _attackChangePercent;//何パーセントで攻撃が変化するか
    [Tooltip("LowHpの小攻撃のアニメーションの名前")]
    public string _attackEventLowHp1AnimState1;//低体力時の小攻撃時のアニメーションのステート名
    [Tooltip("LowHpの小攻撃")]
    [SerializeField] UnityEvent _attackEventHP1 = null;//低体力の小攻撃
    [Tooltip("中攻撃のアニメーションの名前")]
    public string _attackEventLowHp2AnimState1;//低体力時の中攻撃時のアニメーションのステート名
    [Tooltip("LowHpの中攻撃")]
    [SerializeField] UnityEvent _attackEventHP2 = null;//低体力時の中攻撃
    [Tooltip("LowHpの大攻撃のアニメーションの名前")]
    public string _attackEventLowHp3AnimState1;//低体力時の大攻撃時のアニメーションのステート名
    [Tooltip("HPLowの大攻撃")]
    [SerializeField] UnityEvent _attackEventHP3 = null;//低体力時の大攻撃
    void Start()
    {
        _anim = GetComponent<Animator>();
        _lifeSystem = GetComponent<LifeSystem>();
    }

    void Update()
    {
        if(_isStartAnim) StartCoroutine(StartAnim());
        if(_isStartAnim == false)
        {
            if (!_isCoolDown && !_attackChangeHP) StartCoroutine(StartAttack());
            else if (!_isCoolDown && _attackChangeHP) StartHpAttack();
        }
    }

    public IEnumerator StartAnim()
    {
        float animTime;
        _cameraAnim.Play(_startAnimName);
        _startEvent.Invoke();
        yield return null;
        animTime = _cameraAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animTime);
        _isStartAnim = false;
    }
    public IEnumerator StartRest()
    {
        float animTime;
        _anim.Play(_restAnimName);
        _restEvent?.Invoke();
        yield return null;
        animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animTime);
    }

    public void StartHpAttack()
    {
        if ((float)(_lifeSystem.HP / _lifeSystem.MaxHP) * 100 <= _attackChangePercent)
        {
            StartCoroutine(StartAttack());
        }
        else
        {
            StartCoroutine(StartLowHpAttack());
        }
    }


    IEnumerator StartAttack()
    {
        float animTime;
        _isCoolDown = true;
        if (_nowAttackCount2To3 == _attackCount2To3)//中攻撃を一定回数行うと大攻撃を行う
        {
            _nowAttackCount2To3 = 0;
            _anim.Play(_attackEvent1AnimState3);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEvent3?.Invoke();
            Debug.Log("3攻撃");
            StartCoroutine(StartRest());
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
        }
        else if (_nowAttackCount1To2 == _attackCount1To2) //一定回数小攻撃を行うと中攻撃を行う
        {
            _nowAttackCount1To2 = 0;
            _anim.Play(_attackEvent1AnimState2);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEvent2?.Invoke();
            Debug.Log("2攻撃");
            StartCoroutine(StartRest());
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount2To3++;
        }
        else if (_nowAttackCount1To2 != _attackCount1To2) //小攻撃を行うのと小攻撃の回数を記録する
        {
            _anim.Play(_attackEvent1AnimState1);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEvent1?.Invoke();
            Debug.Log("1攻撃");
            StartCoroutine(StartRest());
            yield return new WaitForSeconds(_attackCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount1To2++;
        }
    }
    IEnumerator StartLowHpAttack()
    {
        _isCoolDown = true;
        float animTime;
        if (_nowAttackCount2To3 == _attackCount2To3)//中攻撃を一定回数行うと大攻撃を行う
        {
            _nowAttackCount2To3 = 0;
            _anim.Play(_attackEventLowHp3AnimState1);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEventHP3?.Invoke();
            Debug.Log("LowHP3攻撃");
            StartCoroutine(StartRest());
            yield return new WaitForSeconds(_LowHPattacCoolDownTime);
            _isCoolDown = false;
        }
        else if (_nowAttackCount1To2 == _attackCount1To2) //一定回数小攻撃を行うと中攻撃を行う
        {
            _nowAttackCount1To2 = 0;
            _anim.Play(_attackEventLowHp2AnimState1);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEventHP2?.Invoke();
            Debug.Log("LowHP2攻撃");
            StartCoroutine(StartRest());
            yield return new WaitForSeconds(_LowHPattacCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount2To3++;
        }
        else if (_nowAttackCount1To2 != _attackCount1To2) //小攻撃を行うのと小攻撃の回数を記録する
        { 
            _anim.Play(_attackEventLowHp1AnimState1);
            yield return null;
            animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animTime);
            _attackEventHP1?.Invoke();
            Debug.Log("LowHP1攻撃");
            StartCoroutine(StartRest());
            yield return new WaitForSeconds(_LowHPattacCoolDownTime);
            _isCoolDown = false;
            _nowAttackCount1To2++;
        }
    }
}
