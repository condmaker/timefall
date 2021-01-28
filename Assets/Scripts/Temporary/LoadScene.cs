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

}
