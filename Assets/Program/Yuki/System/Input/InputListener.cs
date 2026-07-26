using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour
{
    [SerializeField]
    private KeyConfigData _keyConfigData;
    [SerializeField]
    private List<ObservableInput> _observableInputs;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [Serializable]
    private struct ObservableInput
    {
        public string name;
        public InputType input;
        public InputState state;
        public List<InputFilter> inputFilters;
        public UnityEvent action;
    }

    [Serializable]
    private struct InputFilter
    {
        [SerializeReference, SubclassSelector]
        public IInputFilter inputFilter;
    }
}
