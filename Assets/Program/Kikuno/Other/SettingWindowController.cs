using UnityEngine;

public class SettingWindowController : MonoBehaviour
{
    [SerializeField] private GameObject _settingWindow;
    [SerializeField] private Animator _animator;

    public void OpenSettingWindow()
    {
        _settingWindow.SetActive(true);

        _animator.Play("SettingWindow_Open");
    }

    public void CloseSettingWindow()
    {
        _animator.Play("SettingWindow_Close");
    }

    public void HideSettingWindow()
    {
        _settingWindow.SetActive(false);
    }
}
