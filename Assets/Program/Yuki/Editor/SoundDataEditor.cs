using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// キーバインドのインスペクター拡張
/// </summary>
[CustomEditor(typeof(SoundData))]
public class SoundDataEditor : Editor
{
    private SoundData _soundData;

    private bool _openBGMs = false;
    private bool _openSEs = false;

    void OnEnable()
    {
        _soundData = target as SoundData;
        SoundListInitialize(_soundData.BGMList, typeof(BGMType));
        SoundListInitialize(_soundData.SEList, typeof(SEType));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if(_openBGMs = EditorGUILayout.Foldout(_openBGMs, new GUIContent("BGM")))
        {
            DrawSoundList(serializedObject.FindProperty("_bgmList"), i => ((BGMType)i).ToString());
        }

        if(_openSEs = EditorGUILayout.Foldout(_openSEs, new GUIContent("SE")))
        {
            DrawSoundList(serializedObject.FindProperty("_seList"), i => ((SEType)i).ToString());
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void SoundListInitialize(List<AudioClip> soundList, Type soundEnum)
    {
        var soundKinds = Enum.GetValues(soundEnum).Length;
        var soundCountDifference = soundList.Count - soundKinds;

        if(soundCountDifference < 0)
        {
            soundList.AddRange(new AudioClip[Mathf.Abs(soundCountDifference)]);
        }
        else if(soundCountDifference > 0)
        {
            soundList.RemoveRange(soundKinds - 1, soundCountDifference);
        }
    }

    private void DrawSoundList(SerializedProperty listProperty, Func<int, string> elemntNameGetter)
    {
        for(int i = 0; i < listProperty.arraySize; i++)
        {
            EditorGUILayout.PropertyField(listProperty.GetArrayElementAtIndex(i), new GUIContent(elemntNameGetter(i)));
            EditorGUILayout.Space();
        }
    }
}
