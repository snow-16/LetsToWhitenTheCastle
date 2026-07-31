using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private GameObject _Window;
    [SerializeField] private Animator animator;

    public void OpenWindow()
    {
        _Window.SetActive(true);

        animator.Play("Window_Open");
    }

    public void CloseWindow()
    {
        animator.Play("Window_Close");
    }

    public void HideWindow()
    {
        _Window.SetActive(false);
    }
}
