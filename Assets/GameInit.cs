using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        int checker = PlayerPrefs.GetInt("f_Check");
        if (checker == 0)
            SceneManager.LoadScene("MainMenu - Medieval");
        else
            SceneManager.LoadScene("MainMenu - Futuristic");

    }
}
