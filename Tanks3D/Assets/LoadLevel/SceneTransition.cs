using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Text LoadingPercent;
    private Animator compomemtAnimator;
    private AsyncOperation loadSceneOperation;

    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

   public static void SwitchToScene(int sceneId)
    {
        instance.compomemtAnimator.SetTrigger("close");
        instance.loadSceneOperation = SceneManager.LoadSceneAsync(sceneId);
        instance.loadSceneOperation.allowSceneActivation = false;
    }
    private void Start()
    {
        instance = this;

        compomemtAnimator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation == true)
        {
            compomemtAnimator.SetTrigger("open");
        }
    }

private void Update()
    {
        if (loadSceneOperation != null)
            LoadingPercent.text = Mathf.RoundToInt(instance.loadSceneOperation.progress * 100) + "%";
    }
    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        loadSceneOperation.allowSceneActivation = true;
    }
}
