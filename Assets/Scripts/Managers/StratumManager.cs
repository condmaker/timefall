using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StratumManager: MonoBehaviour
{
    static public StratumManager instance;
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
