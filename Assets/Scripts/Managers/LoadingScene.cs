using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private NextScene sceneToLoad;

    private AsyncOperation loadingInfo;
    private Slider slider;

    private void Awake()
    {
        SceneManager.LoadSceneAsync(sceneToLoad.SceneString);
    }

    private void Update() =>
        slider.value = Mathf.Clamp01(loadingInfo.progress / 0.9f);
}
