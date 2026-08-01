using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _time;
    private bool _isTiming;

    GameObject _boss;
    BossAI _bossAI;
    public float ClearTime => _time;

    private void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");

        _bossAI = _boss.GetComponent<BossAI>();
    }
    private void Update()
    {
        if (_isTiming)
        {
            _time += Time.deltaTime;

            // UIに時間を表示
            _timerText.text = "TIME : " + _time.ToString("F2");
        }
        else if (_bossAI._isStartAnim == false)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        _time = 0f;
        _isTiming = true;

        _timerText.text = "TIME : 0.0";
    }

    public void StopTimer()
    {
        _isTiming = false;

        Debug.Log("クリアタイム：" + _time);
    }
}