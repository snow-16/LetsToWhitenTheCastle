using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    [SerializeField]
    private KeyConfigData _keyConfigData;
    [SerializeField]
    private InputType _listeningType;
    public InputType ListeningType { get => _listeningType; set => _listeningType = value; }
    [SerializeField]
    private List<ObservableInput> _observableInputs = new();
    public List<ObservableInput> ObservableInputs { get => _observableInputs; set => _observableInputs = value; }

    void Start()
    {
        for(int i = 0; i < _observableInputs.Count; i++)
        {
            if((_listeningType & (InputType)(1 << i)) > 0)
            {
                SubscribeInputs((InputType)(1 << i));
            }
        }
    }

    private void SubscribeInputs(InputType inputType)
    {
        var input = _keyConfigData.KeyConfigs[(int)Mathf.Log((int)inputType, 2)].input;
        input.Enable();

        Observable.EveryUpdate()
        .Where(_ => input.WasPressedThisFrame() || input.WasReleasedThisFrame() || input.IsPressed())
        .Subscribe(_ =>
            {
                foreach(var inputSetting in _observableInputs[(int)Mathf.Log((int)inputType, 2)].inputSettings)
                {
                    if(FireInput(inputSetting, input))
                    {
                        inputSetting.action.Invoke(input.ReadValue<float>());
                        break;
                    }
                };
            }
        );
    }

    private bool FireInput(InputSetting inputSetting, InputAction input)
    {
        if(inputSetting.inputFilters.Count == 0 || inputSetting.inputFilters.All(item => item.inputFilter.WhenCanInput()))
        {
            switch(inputSetting.state)
            {
                case InputState.Pressed :
                {
                    return input.WasPressedThisFrame();
                }
                case InputState.Pressing :
                {
                    return input.IsPressed();
                }
                case InputState.Released :
                {
                    return input.WasReleasedThisFrame();
                }
                case InputState.PressOrReleased :
                {
                    return input.WasPressedThisFrame() || input.WasReleasedThisFrame();
                }
            }
        }

        return false;
    }

    [Serializable]
    public struct ObservableInput
    {
        public List<InputSetting> inputSettings;
    }

    [Serializable]
    public struct InputSetting
    {
        public string name;
        public InputState state;
        public List<InputFilter> inputFilters;
        public ValueEvent action;

        [Serializable] public class ValueEvent : UnityEvent<float> { }
    }

    [Serializable]
    public struct InputFilter
    {
        [SerializeReference, SubclassSelector]
        public IInputFilter inputFilter;
    }
}
