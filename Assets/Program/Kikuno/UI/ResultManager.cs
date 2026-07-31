using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [Header("基本設定")]
    [SerializeField] private int baseScore = 3000;
    [SerializeField] private int timeMultiplier = 30;

    [Header("ボーナス")]
    [SerializeField] private int noDamageBonus = 1700;
    [SerializeField] private int damagePenalty = 300;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI baseScoreText;
    [SerializeField] private TextMeshProUGUI timeBonusText;
    [SerializeField] private TextMeshProUGUI noDamageBonusText;
    [SerializeField] private TextMeshProUGUI damagePenaltyText;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    public void SetResult(float clearTime, int damageCount)
    {
        // タイムボーナス
        int timeBonus =
            Mathf.Max(0, (baseScore - Mathf.FloorToInt(clearTime)) * timeMultiplier);

        // ノーダメージボーナス
        int noDamage = damageCount == 0 ? noDamageBonus : 0;

        // 被弾ペナルティ
        int penalty = damageCount * damagePenalty;

        // 合計
        int totalScore =
            baseScore + timeBonus + noDamage - penalty;

        // 表示
        baseScoreText.text = baseScore.ToString();
        timeBonusText.text = "+" + timeBonus.ToString();
        noDamageBonusText.text = "+" + noDamage.ToString();
        damagePenaltyText.text = "-" + penalty.ToString();
        totalScoreText.text = totalScore.ToString();
    }
}