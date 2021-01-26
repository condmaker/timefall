using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuToClose;
    [SerializeField]
    private GameObject menuToOpen;


    public void SwitchMenus()
    {
        if(menuToClose != null)
            menuToClose.SetActive(false);

        menuToOpen.SetActive(true);
    }
}
