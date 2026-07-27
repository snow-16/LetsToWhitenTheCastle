using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
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
        _observableInputs.ForEach(observer =>
            {
                var input = _keyConfigData.KeyConfigs[(int)observer.type].input;

                input.Enable();
                Observable.EveryUpdate()
                .Where(_ => input.IsPressed())
                .Where(_ => observer.inputFilters.All(item => item.inputFilter.WhenCanInput()))
                .Subscribe(_ =>
                    {
                        observer.action.Invoke(input.ReadValue<float>());
                    }
                ).AddTo(this);
            }
        );
    }

    [Serializable]
    private struct ObservableInput
    {
        public string name;
        public InputType type;
        public InputState state;
        public List<InputFilter> inputFilters;
        public ValueEvent action;

        [Serializable] public class ValueEvent : UnityEvent<float> { }
    }

    [Serializable]
    private struct InputFilter
    {
        [SerializeReference, SubclassSelector]
        public IInputFilter inputFilter;
    }
}
