using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// HPを管理するコンポーネント
/// </summary>
public class LifeSystem : MonoBehaviour
{
    /// <summary> 最大HP </summary>
    [SerializeField]
    [Tooltip("最大HPです。")]
    private int _maxHP;
    /// <summary> 最大HP </summary>
    public int MaxHP { get => _maxHP; set => _maxHP = value; }
    /// <summary> 死亡時の処理 </summary>
    [SerializeField]
    private UnityEvent _whenDead;

    /// <summary> 現在のHP </summary>
    private int _hp;
    /// <summary> 現在のHP </summary>
    public int HP { get => _hp; set => _hp = value; }

    void Start()
    {
        _hp = _maxHP;
    }

    /// <summary>
    /// HPを増減させるメソッド
    /// </summary>
    /// <param name="value">変化値</param>
    public void FluctuationHP(int value)
    {
        _hp = Mathf.Max(_hp - value, 0);

        if(_hp <= 0)
        {
            _whenDead?.Invoke();
        }
    }
}
