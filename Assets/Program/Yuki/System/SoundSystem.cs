using UnityEngine;

/// <summary>
/// サウンドの管理を行うコンポーネント
/// </summary>
[DefaultExecutionOrder(-100)]
public class SoundSystem : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private SoundData _soundData;
    /// <summary> BGM用AudioSource </summary>
    [SerializeField]
    private AudioSource _bgmSource;
    /// <summary> SE用AudioSource </summary>
    [SerializeField]
    private AudioSource _seSource;

    /// <summary> 自身のインスタンス </summary>
    public static SoundSystem Instance { get; private set; }

    void Start()
    {
        if(FindObjectsByType<SoundSystem>(FindObjectsSortMode.None).Length == 1)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="bgm">再生するBGM</param>
    public void PlayBGM(BGMType bgm)
    {
        _bgmSource.clip = _soundData.BGMList[(int)bgm];
        _bgmSource.Play();
    }

    /// <summary>
    /// BGMを停止する
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="se">再生するSE</param>
    public void PlaySE(SEType se)
    {
        _seSource.PlayOneShot(_soundData.SEList[(int)se]);
    }
}
