using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// 各種入力処理用コンポーネント
/// </summary>
public class InputListener : MonoBehaviour
{
    /// <summary> キーバインドを保管したScriptableObject </summary>
    [SerializeField]
    private KeyBindData _keyBindData;

    /// <summary> 受け付ける入力の種類 </summary>
    [SerializeField]
    private InputType _listeningType;
    /// <summary> 受け付ける入力の種類 </summary>
    public InputType ListeningType { get => _listeningType; set => _listeningType = value; }

    /// <summary> 入力条件と入力結果のリスト </summary>
    [SerializeField]
    private List<ObservableInput> _observableInputs = new();
    /// <summary> 入力条件と入力結果のリスト </summary>
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

    /// <summary>
    /// 入力を監視するストリームを設定
    /// </summary>
    /// <param name="inputType">監視先の入力</param>
    private void SubscribeInputs(InputType inputType)
    {
        var input = _keyBindData.KeyBinds[(int)Mathf.Log((int)inputType, 2)].input;
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

    /// <summary>
    /// 入力確認後、条件によるふるい分けと処理実行
    /// </summary>
    /// <param name="inputSetting">実行可能か確認する入力処理</param>
    /// <param name="input">入力対象のInputAction</param>
    /// <returns>処理を実行したか</returns>
    private bool FireInput(InputSetting inputSetting, InputAction input)
    {
        //入力を通す条件を満たしているか確認
        if(inputSetting.inputFilters.Count == 0 || inputSetting.inputFilters.All(item => item.inputFilter.IsCanInput()))
        {
            //指定した入力方法に合った入力形式になっているか確認
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

    /// <summary>
    /// 条件別で処理のリストを保管する構造体
    /// </summary>
    [Serializable]
    public struct ObservableInput
    {
        /// <summary> 条件別処理リスト </summary>
        public List<InputSetting> inputSettings;
    }

    /// <summary>
    /// 処理条件と処理内容を保管する構造体
    /// </summary>
    [Serializable]
    public struct InputSetting
    {
        /// <summary> この処理の名前 </summary>
        public string name;
        /// <summary> 入力の仕方 </summary>
        public InputState state;
        /// <summary> 入力を通す条件 </summary>
        public List<InputFilter> inputFilters;
        /// <summary> 処理内容 </summary>
        public ValueEvent action;

        /// <summary> 入力結果を処理に渡すためのカスタムイベント </summary>
        [Serializable] public class ValueEvent : UnityEvent<float> { }
    }

    /// <summary>
    /// 処理を実行させるかどうかの条件のリストを保管する構造体
    /// </summary>
    [Serializable]
    public struct InputFilter
    {
        /// <summary> 処理の実行可否の条件のリスト </summary>
        [SerializeReference, SubclassSelector]
        public IInputFilter inputFilter;
    }
}
