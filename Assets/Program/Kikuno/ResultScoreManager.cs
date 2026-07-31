using UnityEngine;

public class ResultScoreManager : MonoBehaviour
{
    [Header("タイムボーナス")]
    [Tooltip("タイムボーナスの基準となる点数")]
    [SerializeField] private int _basePoint = 5000;

    [Tooltip("クリア時間1秒あたりの倍率")]
    [SerializeField] private int _timeMultiplier = 100;

    [Header("ノーダメージボーナス")]
    [SerializeField] private int _noDamageBonus = 1700;

    [Header("被弾ペナルティ")]
    [SerializeField] private int _damagePenalty = 300;

    /// <summary>
    /// 最終スコアを計算
    /// </summary>
    public int CalculateScore(float clearTime, int damageCount)
    {
        // タイムボーナス
        int timeBonus = Mathf.Max(
            (_basePoint - Mathf.FloorToInt(clearTime)) * _timeMultiplier,
            0
        );

        // ノーダメージボーナス
        int noDamageBonus = 0;

        if (damageCount == 0)
        {
            noDamageBonus = _noDamageBonus;
        }

        // 被弾ペナルティ
        int damagePenalty = damageCount * _damagePenalty;

        // 最終スコア
        int totalScore =
            timeBonus +
            noDamageBonus -
            damagePenalty;

        return Mathf.Max(totalScore, 0);
    }
}