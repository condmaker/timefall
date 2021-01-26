using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{
    private TextMeshProUGUI textToDisplay;
    [SerializeField]
    private Sprite grabIcon, interactIcon, talkIcon, exitIcon;
    private Image iconDisplay;

    private Color colorNone;
    private Color colorFull;

    void Start()
    {
        iconDisplay = GetComponentInChildren<Image>();
        iconDisplay.sprite = null;
        //unity made me do this...
        colorNone = iconDisplay.color;
        colorFull = iconDisplay.color;
        colorNone.a = 0;
    }

    /// <summary>
    /// Method responsible for display the interaction icon on the screen
    /// </summary>
    /// <param name="_objectData"></param>
    public void DisplayMessage(ObjectData _objectData)
    {
        switch (_objectData?.InteractionType)
        {
            case InteractionType.isGrabable:
                iconDisplay.sprite = grabIcon;
                iconDisplay.color = colorFull;
                break;
            case InteractionType.isUsable:

                iconDisplay.sprite = interactIcon;
                iconDisplay.color = colorFull;
                break;
            case InteractionType.isNPC:
                iconDisplay.sprite = talkIcon;
                iconDisplay.color = colorFull;
                break;
            case InteractionType.isExit:
                iconDisplay.sprite = exitIcon;
                iconDisplay.color = colorFull;
                break;
            default:
                iconDisplay.sprite = null;
                iconDisplay.color = colorNone;
                break;
        }
    }

    /// <summary>
    /// Method responsible for cleaning the displayed icon
    /// </summary>
    public void CleanMessage()
    {
        iconDisplay.sprite = null;
        iconDisplay.color = colorNone;
    }
}
