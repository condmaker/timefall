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
            SceneManager.LoadScene(sceneName);
        }
        catch (Exception)
        {
            print("Scene name invalid!");
        }
    }

}
