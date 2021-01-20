using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStratum : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private NextScene nextScene;

    public void Next()
    {
        try
        {
            nextScene.SceneString = sceneName;
            // Change to index later
            SceneManager.LoadScene("Loading");
        }
        catch (Exception)
        {
            print("Scene name invalid!");
        }
    }

}
