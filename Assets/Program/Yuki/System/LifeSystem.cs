using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// HPを管理するコンポーネント
/// </summary>
public class LifeSystem : MonoBehaviour
{
    /// <summary> 最大HP </summary>
    [SerializeField]
    private int _maxHP;
    /// <summary> 死亡時の処理 </summary>
    [SerializeField]
    private UnityEvent _whenDead;

    /// <summary> 現在のHP </summary>
    private int _hp;

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
        _whenDead?.Invoke();
    }
}
