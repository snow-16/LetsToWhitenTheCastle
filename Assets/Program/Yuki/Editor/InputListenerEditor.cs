using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 入力受け付けのインスペクター拡張
/// </summary>
[CustomEditor(typeof(InputListener))]
public class InputListenerEditor : Editor
{
    private InputListener _inputListener;
    private SerializedProperty _keyBindData;
    private SerializedProperty _observableInputs;

    void OnEnable()
    {
        _inputListener = target as InputListener;
        int inputKinds = Enum.GetValues(typeof(InputType)).Length;
        int listCountDifference = _inputListener.ObservableInputs.Count - inputKinds;
        _keyBindData = serializedObject.FindProperty("_keyBindData");
        _observableInputs = serializedObject.FindProperty("_observableInputs");

        if(listCountDifference < 0)
        {
            _inputListener.ObservableInputs.AddRange(new InputListener.ObservableInput[Mathf.Abs(listCountDifference)]);
        }
        else if(listCountDifference > 0)
        {
            _inputListener.ObservableInputs.RemoveRange(inputKinds - 1, listCountDifference);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _inputListener.ListeningType = (InputType)EditorGUILayout.EnumFlagsField("受け付ける入力", _inputListener.ListeningType);
        EditorGUILayout.PropertyField(_keyBindData, new GUIContent("キーバインド"));

        for(int i = 0; i < _inputListener.ObservableInputs.Count; i++)
        {
            if((_inputListener.ListeningType & (InputType)(1 << i)) > 0)
            {
                var observableInput = _observableInputs.GetArrayElementAtIndex(i).FindPropertyRelative("inputSettings");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("上位の入力が優先され、反応時は下位の入力は無視されます。");
                EditorGUILayout.PropertyField(observableInput, new GUIContent(((InputType)(1 << i)).ToString()));
                EditorGUILayout.Space();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
