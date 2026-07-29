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
    [Tooltip("各種キーバインドの設定です。お好きに調整してください。")]
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
