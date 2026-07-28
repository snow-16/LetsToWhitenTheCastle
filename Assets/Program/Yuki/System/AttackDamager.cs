using UnityEngine;

/// <summary>
/// HPを持つ相手にダメージを与えるコンポーネント
/// </summary>
public class AttackDamager : MonoBehaviour
{
    /// <summary> 与えるダメージ </summary>
    [SerializeField]
    [Tooltip("与えるダメージ量です。")]
    private int _damage;

    /// <summary>
    /// ダメージを与えるメソッド
    /// </summary>
    /// <param name="target">攻撃対象</param>
    public void Attack(LifeSystem target)
    {
        target.FluctuationHP(_damage);
    }
}
