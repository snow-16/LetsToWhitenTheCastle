using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundData")]
public class SoundData : ScriptableObject
{
    /// <summary> 各種BGMのデータ </summary>
    [SerializeField]
    [Tooltip("BGMの種類とサウンドデータです。")]
    private List<AudioClip> _bgmList = new();
    /// <summary> 各種BGMのデータ </summary>
    public List<AudioClip> BGMList => _bgmList;

    /// <summary> 各種SEのデータ </summary>
    [SerializeField]
    [Tooltip("SEの種類とサウンドデータです。")]
    private List<AudioClip> _seList = new();
    /// <summary> 各種SEのデータ </summary>
    public List<AudioClip> SEList => _seList;
}
