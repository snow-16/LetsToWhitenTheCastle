using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// キーバインドを設定するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "KeyBindData", menuName = "Scriptable Objects/KeyBindData")]
public class KeyBindData : ScriptableObject
{
    /// <summary> キーバインドのリスト </summary>
    [SerializeField]
    private List<KeyBind> _keyBinds = new();
    /// <summary> キーバインドのリスト </summary>
    public List<KeyBind> KeyBinds { get => _keyBinds; set => _keyBinds = value; }

    /// <summary>
    /// キーバインドの内容
    /// </summary>
    [Serializable]
    public struct KeyBind
    {
        public InputType type;
        public InputAction input;
    }
}
