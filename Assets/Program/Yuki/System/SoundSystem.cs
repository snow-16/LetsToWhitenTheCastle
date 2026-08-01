using UnityEngine;

/// <summary>
/// サウンドの管理を行うコンポーネント
/// </summary>
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
}
