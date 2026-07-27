using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// キーバインドのインスペクター拡張
/// </summary>
[CustomEditor(typeof(KeyBindData))]
public class KeyBindDataEditor : Editor
{
    private KeyBindData _keyBindData;

    void OnEnable()
    {
        _keyBindData = target as KeyBindData;
        int inputKinds = Enum.GetValues(typeof(InputType)).Length;
        int listCountDifference = _keyBindData.KeyBinds.Count - inputKinds;

        if(listCountDifference < 0)
        {
            _keyBindData.KeyBinds.AddRange(new KeyBindData.KeyBind[Mathf.Abs(inputKinds)]);
        }
        else if(listCountDifference > 0)
        {
            _keyBindData.KeyBinds.RemoveRange(inputKinds - 1, listCountDifference);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var keyConfigs = serializedObject.FindProperty("_keyBinds");
        
        for(int i = 0; i < _keyBindData.KeyBinds.Count; i++)
        {
            var keyConfig = keyConfigs.GetArrayElementAtIndex(i);
            EditorGUILayout.LabelField(((InputType)(1 << i)).ToString());
            EditorGUILayout.PropertyField(keyConfig.FindPropertyRelative("input"));
            EditorGUILayout.Space();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
