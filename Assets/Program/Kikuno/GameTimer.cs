using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float _time;
    private bool _isTiming;

    // 現在のクリア時間
    public float ClearTime => _time;

    private void Update()
    {
        if (_isTiming)
        {
            _time += Time.deltaTime;
        }
    }

    // 計測開始
    public void StartTimer()
    {
        _time = 0f;
        _isTiming = true;
    }

    // 計測終了
    public void StopTimer()
    {
        _isTiming = false;
    }
}