using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 現在値と最大値をゲージに表示するコンポーネント
/// </summary>
public class GaugeUI : MonoBehaviour
{
    /// <summary> ゲージのImage </summary>
    [SerializeField]private Image _fillImage;

    /// <summary>ゲージを更新する</summary>
    /// <param name="currentValue">現在値</param>
    /// <param name="maxValue">最大値</param>
    public void UpdateGauge(int currentValue, int maxValue)
    {
        _fillImage.fillAmount = (float)currentValue / maxValue;
    }
}
