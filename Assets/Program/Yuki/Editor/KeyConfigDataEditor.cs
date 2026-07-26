using System;
using Unity.VisualScripting;
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
            _keyConfigData.KeyConfigs.AddRange(new KeyConfigData.KeyConfig[inputKinds]);
        }
        else if(listCountDifference > 0)
        {
            _keyConfigData.KeyConfigs.RemoveRange(inputKinds - 1, listCountDifference);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for(int i = 0; i < _keyConfigData.KeyConfigs.Count; i++)
        {
            var keyConfig = serializedObject.FindProperty("_keyConfigs").GetArrayElementAtIndex(i);
            EditorGUILayout.LabelField(((InputType)i).ToString());
            EditorGUILayout.PropertyField(keyConfig.FindPropertyRelative("input"));
            EditorGUILayout.Space();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
