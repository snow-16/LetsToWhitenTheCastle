using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "KeyConfigData", menuName = "Scriptable Objects/KeyConfigData")]
public class KeyConfigData : ScriptableObject
{
    [SerializeField]
    private List<KeyConfig> KeyConfigs;

    [Serializable]
    public struct KeyConfig
    {
        public InputType type;
        public InputAction input;
    }
}
