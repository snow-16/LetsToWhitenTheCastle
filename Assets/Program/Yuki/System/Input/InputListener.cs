using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using MackySoft.SerializeReferenceExtensions;

public class InputListener : MonoBehaviour
{
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
        public InputAction key;
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
