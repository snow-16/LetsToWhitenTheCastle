using UnityEngine;

public class SettingWindowController : MonoBehaviour
{
    [SerializeField] private GameObject settingWindow;
    [SerializeField] private Animator animator;

    public void OpenSettingWindow()
    {
        settingWindow.SetActive(true);

        animator.Play("SettingWindow_Open");
    }

    public void CloseSettingWindow()
    {
        animator.Play("SettingWindow_Close");
    }

    public void HideSettingWindow()
    {
        settingWindow.SetActive(false);
    }
}
