using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LifeSystemのHPを体力ゲージに表示するコンポーネント
/// </summary>
public class GaugeUI : MonoBehaviour
{
    /// <summary> HPや忍術ゲージを管理しているSystem </summary>
    [SerializeField]
    private LifeSystem _System;

    /// <summary> HPや忍術ゲージのImage </summary>
    [SerializeField]
    private Image _fillImage;

    private void Update()
    {
        // 現在HP ÷ 最大HP でゲージの割合を計算
        _fillImage.fillAmount = (float)_System.HP / _System.MaxHP;
    }
}