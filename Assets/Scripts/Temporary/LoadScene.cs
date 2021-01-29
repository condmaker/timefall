using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads a new scene.
/// </summary>
public class LoadScene : MonoBehaviour
{
    /// <summary>
    /// Name of the scene.
    /// </summary>
    public string scene;

    /// <summary>
    /// Passes the scene name to the SceneManager to load it.
    /// </summary>
    public void PassScene()
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Passes the scene name to the SceneManager to load it if the Futuristic
    /// Check is 1 (true).
    /// </summary>
    public void PassSceneFCheck()
    {
        if (PlayerPrefs.GetInt("F_Check") >= 1)
            SceneManager.LoadScene(scene);
    }

}
