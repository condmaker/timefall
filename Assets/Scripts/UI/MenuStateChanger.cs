using UnityEngine;

/// <summary>
/// Class responsible for changing the state of a menu
/// </summary>
public class MenuStateChanger : MonoBehaviour
{
    /// <summary>
    /// Menu to change the state of
    /// </summary>
    [SerializeField]
    private GameObject menu = null;

    /// <summary>
    /// Method responsible for activating the menu
    /// </summary>
    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    /// <summary>
    /// Method responsible for deactivating the menu
    /// </summary>
    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    /// <summary>
    /// Method called once per frame
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
