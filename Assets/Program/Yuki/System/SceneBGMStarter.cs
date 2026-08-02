using UnityEngine;

/// <summary>
/// BGMの管理を行うコンポーネント
/// </summary>
public class SceneBGMStarter : MonoBehaviour
{
    /// <summary> このシーンで流すBGM </summary>
    [SerializeField]
    private BGMType _sceneBGM;

    void Start()
    {
        SoundSystem.Instance.PlayBGM(_sceneBGM);
    }
}
