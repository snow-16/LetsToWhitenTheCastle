using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyConfigData))]
public class KeyConfigDataEditor : Editor
{
    private KeyConfigData _keyConfigData;

    void OnEnable()
    {
        _keyConfigData = target as KeyConfigData;
        int inputKinds = Enum.GetValues(typeof(InputType)).Length;
        int listCountDifference = _keyConfigData.KeyConfigs.Count - inputKinds;

        if(listCountDifference < 0)
        {
            _keyConfigData.KeyConfigs.AddRange(new KeyConfigData.KeyConfig[Mathf.Abs(inputKinds)]);
        }
        else if(listCountDifference > 0)
        {
            _keyConfigData.KeyConfigs.RemoveRange(inputKinds - 1, listCountDifference);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var keyConfigs = serializedObject.FindProperty("_keyConfigs");
        
        for(int i = 0; i < _keyConfigData.KeyConfigs.Count; i++)
        {
            var keyConfig = keyConfigs.GetArrayElementAtIndex(i);
            EditorGUILayout.LabelField(((InputType)(1 << i)).ToString());
            EditorGUILayout.PropertyField(keyConfig.FindPropertyRelative("input"));
            EditorGUILayout.Space();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
