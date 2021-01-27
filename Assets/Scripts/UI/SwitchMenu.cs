using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class responsible for switching between menus
/// </summary>
public class SwitchMenu : MonoBehaviour
{
    /// <summary>
    /// The object to close
    /// </summary>
    [SerializeField]
    private GameObject menuToClose;

    /// <summary>
    /// The object to open
    /// </summary>
    [SerializeField]
    private GameObject menuToOpen;


    /// <summary>
    /// Method responsible for switching between menus
    /// </summary>
    public void SwitchMenus()
    {
        if(menuToClose != null)
            menuToClose.SetActive(false);

        menuToOpen.SetActive(true);
    }
}
