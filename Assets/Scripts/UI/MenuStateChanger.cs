using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void ChangeMenuState()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
