using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for initiating the correct Main Menu according to 
/// player progression.
/// </summary>
public class GameInit : MonoBehaviour
{
    /// <summary>
    /// Method that checks player progression and loads the respective Main 
    /// Menu.
    /// </summary>
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("firstT"))
        {
            PlayerPrefs.SetInt("fCheck", 0);
            PlayerPrefs.SetInt("firstT", 0);
        }

        int checker = PlayerPrefs.GetInt("fCheck");
        if (checker == 0)
            SceneManager.LoadScene("MainMenu - Medieval");
        else if(checker == 1)
            SceneManager.LoadScene("MainMenu - Futuristic");

    }
}
