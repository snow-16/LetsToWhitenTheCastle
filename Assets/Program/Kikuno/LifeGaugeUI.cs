using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LifeSystemのHPを体力ゲージに表示するコンポーネント
/// </summary>
public class LifeGaugeUI : MonoBehaviour
{
    /// <summary> HPを管理しているLifeSystem </summary>
    [SerializeField]
    private LifeSystem _lifeSystem;

    /// <summary> HPゲージのImage </summary>
    [SerializeField]
    private Image _fillImage;

    private void Update()
    {
        // 現在HP ÷ 最大HP でゲージの割合を計算
        _fillImage.fillAmount =
            (float)_lifeSystem.HP / _lifeSystem.MaxHP;
    }
}