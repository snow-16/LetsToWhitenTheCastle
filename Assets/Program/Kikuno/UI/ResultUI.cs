using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _rankText;


    private void Start()
    {
        _timeText.text =
            "TIME : " + ResultData.Instance.ClearTime.ToString("F2");

        _scoreText.text =
            "SCORE : " + ResultData.Instance.TotalScore;

        _rankText.text =
            "RANK : " + ResultData.Instance.Rank;
    }
}