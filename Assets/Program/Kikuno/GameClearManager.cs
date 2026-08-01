using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private ResultScoreManager _scoreManager;
    [SerializeField] private LifeSystem _playerLife;


    public void GameClear()
    {
        // タイマー停止
        _gameTimer.StopTimer();

        // クリア時間取得
        float time = _gameTimer.ClearTime;

        // スコア計算
        int score = _scoreManager.CalculateScore(
            time,
            _playerLife.HP,
            _playerLife.MaxHP
        );


        // 結果保存
        ResultData.Instance.ClearTime = time;
        ResultData.Instance.HpScore = _scoreManager.HpScore;
        ResultData.Instance.TimeScore = _scoreManager.TimeScore;
        ResultData.Instance.TotalScore = score;
        ResultData.Instance.Rank =
            _scoreManager.GetRank(score);


        // リザルトへ
        SceneManager.LoadScene("ClearScene");
    }
}
