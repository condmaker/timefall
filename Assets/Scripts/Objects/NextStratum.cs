using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStratum : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public void Next()
    {
        try
        {
            StratumManager.instance.SceneString = sceneName;
            // Change to index later
            SceneManager.LoadScene("Loading");
        }
        catch (Exception)
        {
            print("Scene name invalid!");
        }
    }

}
