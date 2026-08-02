using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clearTimeText;
    [SerializeField] private TextMeshProUGUI _hpScoreText;
    [SerializeField] private TextMeshProUGUI _timeBonusText;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private UnityEngine.UI.Image _rankImage;
    [SerializeField] private Sprite _rankS;
    [SerializeField] private Sprite _rankA;
    [SerializeField] private Sprite _rankB;
    [SerializeField] private Sprite _rankC;


    private void Start()
    {
        _clearTimeText.text =
            " ｸﾘｱ ﾀｲﾑ  : " + ResultData.Instance.ClearTime.ToString("F2");


        _hpScoreText.text =
            " HP  ｽｺｱ  : " + ResultData.Instance.HpScore;


        _timeBonusText.text =
            "ﾀｲﾑ ﾎﾞｰﾅｽ : " + ResultData.Instance.TimeScore;


        _totalScoreText.text =
            " ﾄｰﾀﾙ ｽｺｱ : " + ResultData.Instance.TotalScore;


        SetRankImage(ResultData.Instance.Rank);
    }
    private void SetRankImage(string rank)
    {
        switch (rank)
        {
            case "S":
                _rankImage.sprite = _rankS;
                break;

            case "A":
                _rankImage.sprite = _rankA;
                break;

            case "B":
                _rankImage.sprite = _rankB;
                break;

            case "C":
                _rankImage.sprite = _rankC;
                break;
        }
    }
}