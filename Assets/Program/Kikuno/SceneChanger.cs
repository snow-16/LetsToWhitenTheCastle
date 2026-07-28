using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
