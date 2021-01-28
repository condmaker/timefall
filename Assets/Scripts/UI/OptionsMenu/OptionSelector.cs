using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class that represents the component Option Selector
/// This component as various options that can be choosen by pressing 
/// the Right and Left buttons
/// </summary>
public class OptionSelector : MonoBehaviour
{
    /// <summary>
    /// Name of the option.
    /// This will be used to store the information in a PlayerPref
    /// </summary>
    [SerializeField]
    private string optionName = default;


    /// <summary>
    /// Index of the current choosen option
    /// </summary>
    private int index;


    /// <summary>
    /// List of all the available choices
    /// </summary>
    [SerializeField]
    private string[] options = default;


    /// <summary>
    /// Left and Right button to itarate the option
    /// </summary>
    [SerializeField]
    private Button Left = default, Right = default;


    /// <summary>
    /// Text component to display the current option on
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI optionText = null;


    /// <summary>
    /// Event that is triggered when a option is choosen
    /// </summary>
    public Action onSelect;


    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt(optionName,0);
        UpdateButtons();
        Left.onClick.AddListener(GoLeft);
        Right.onClick.AddListener(GoRight);
    }


    /// <summary>
    /// Method responsible for iterating the opitons to the right
    /// </summary>
    public void GoRight()
    {
        index++;
        UpdateButtons();
    }


    /// <summary>
    /// Method responsible for iterating the opitons to the left
    /// </summary>
    public void GoLeft()
    {
        index--;       
        UpdateButtons();
    }


    /// <summary>
    /// Method responsible for updating the state of the component
    /// </summary>
    /// <returns></returns>
    public bool UpdateButtons()
    {
        bool check = true;

        optionText.text = options[index];

        if (index == 0)
        {
            Left.gameObject.SetActive(false);
            check = false;
        }
        else
        {
            Left.gameObject.SetActive(true);
        }

        if (index == options.Length - 1)
        {
            Right.gameObject.SetActive(false);
            check = false;
        }
        else
        {
            Right.gameObject.SetActive(true);
        }

        PlayerPrefs.SetInt(optionName, index);

        onSelect?.Invoke();

        return check;
    }

}
