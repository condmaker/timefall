using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class that has a Scene string as information.
/// </summary>
public class StratumManager: MonoBehaviour
{
    /// <summary>
    /// The current instance on the scene of the StratumManager.
    /// </summary>
    static public StratumManager instance;
    /// <summary>
    /// A string containing the name of a scene. Used in Loading screens to
    /// locate said scene and load it.
    /// </summary>
    public string SceneString { get; set; }

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
