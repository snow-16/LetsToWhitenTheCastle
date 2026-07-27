using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputListener))]
public class InputListenerEditor : Editor
{
    private InputListener _inputListener;

    void OnEnable()
    {
        _inputListener = target as InputListener;
        int inputKinds = Enum.GetValues(typeof(InputType)).Length;
        int listCountDifference = _inputListener.ObservableInputs.Count - inputKinds;

        if(listCountDifference < 0)
        {
            _inputListener.ObservableInputs.AddRange(new InputListener.ObservableInput[Mathf.Abs(inputKinds)]);
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

        var observableInputs = serializedObject.FindProperty("_observableInputs");
        for(int i = 0; i < _inputListener.ObservableInputs.Count; i++)
        {
            if((_inputListener.ListeningType & (InputType)(1 << i)) > 0)
            {
                var observableInput = observableInputs.GetArrayElementAtIndex(i).FindPropertyRelative("inputSettings");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("上位の入力が優先され、反応時は下位の入力は無視されます。");
                EditorGUILayout.PropertyField(observableInput, new GUIContent(((InputType)(1 << i)).ToString()));
                EditorGUILayout.Space();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
