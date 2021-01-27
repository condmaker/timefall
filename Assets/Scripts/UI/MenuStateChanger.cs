using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
