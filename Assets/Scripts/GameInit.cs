using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        int checker = PlayerPrefs.GetInt("F_Check");
        if (checker == 0)
            SceneManager.LoadScene("MainMenu - Medieval");
        else if(checker == 1)
            SceneManager.LoadScene("MainMenu - Futuristic");

    }
}
