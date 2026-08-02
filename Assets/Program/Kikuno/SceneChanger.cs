using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Animator _fadeAnimator;
    // Animator Controller上の「フェードアウトのステート名（ノード名）」を指定
    [SerializeField] private string _fadeOutStateName = "FadeOut";

    public void LoadScene()
    {
        StartCoroutine(ChangeScene());
    }
    private IEnumerator ChangeScene()
    {
        _fadeAnimator.SetTrigger("FadeOut");

        //SetTrigger直後は1フレーム待たないとAnimator内部の状態が更新されないため待機
        yield return null;

        //アニメーションが100%（1.0f）再生されるまで自動で待つ
        yield return new WaitUntil(() =>
        {
            AnimatorStateInfo stateInfo = _fadeAnimator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(_fadeOutStateName) && stateInfo.normalizedTime >= 1.0f;
        });

        SceneManager.LoadScene(_sceneName);
    }

}
