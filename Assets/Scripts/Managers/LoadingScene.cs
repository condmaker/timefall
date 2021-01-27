using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for loading a new Scene
/// </summary>
public class LoadingScene : MonoBehaviour
{
    private void Awake() =>
        SceneManager.LoadSceneAsync(StratumManager.instance.SceneString);
}
