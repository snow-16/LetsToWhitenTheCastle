using UnityEngine;

public class ResultData : MonoBehaviour
{
    public static ResultData Instance;

    public float ClearTime;
    public int HpScore;
    public int TimeScore;
    public int TotalScore;
    public string Rank;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}