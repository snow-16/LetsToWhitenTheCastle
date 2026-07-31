using UnityEngine;

/// <summary>ゲームの残り時間を管理するコンポーネント</summary>
public class TimeManager : MonoBehaviour
{
    /// <summary> 制限時間 </summary>
    [SerializeField]
    private float _maxTime = 120f;

    /// <summary> 現在の残り時間 </summary>
    public float CurrentTime { get; private set; }

    private void Start()
    {
        CurrentTime = _maxTime;
    }

    private void Update()
    {
        CurrentTime -= Time.deltaTime;

        if (CurrentTime < 0)
        {
            CurrentTime = 0;
        }
    }
}