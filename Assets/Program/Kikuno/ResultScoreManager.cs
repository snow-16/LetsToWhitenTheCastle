using UnityEngine;

public class ResultScoreManager : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int _maxHpScore = 5000;

    [Header("タイムボーナス")]
    [SerializeField] private int _time70 = 5000;
    [SerializeField] private int _time80 = 4000;
    [SerializeField] private int _time90 = 3000;
    [SerializeField] private int _time100 = 2000;
    [SerializeField] private int _time110 = 1000;

    public int HpScore { get; private set; }
    public int TimeScore { get; private set; }
    public int TotalScore { get; private set; }


    public int CalculateScore(float clearTime, int currentHp, int maxHp)
    {
        // HPスコア
        float hpRate = (float)currentHp / maxHp;
        HpScore = Mathf.RoundToInt(hpRate * _maxHpScore);


        // タイムボーナス
        TimeScore = 0;

        if (clearTime <= 70)
            TimeScore = _time70;
        else if (clearTime <= 80)
            TimeScore = _time80;
        else if (clearTime <= 90)
            TimeScore = _time90;
        else if (clearTime <= 100)
            TimeScore = _time100;
        else if (clearTime <= 110)
            TimeScore = _time110;


        // 合計
        TotalScore = HpScore + TimeScore;

        return TotalScore;
    }


    public string GetRank(int score)
    {
        if (score >= 8000)
        {
            return "S";
        }
        else if (score >= 7000)
        {
            return "A";
        }
        else if (score >= 5000)
        {
            return "B";
        }
        else
        {
            return "C";
        }
    }
}