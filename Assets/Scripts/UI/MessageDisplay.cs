using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour
{
    private TextMeshProUGUI textToDisplay;
    [SerializeField]
    private Sprite grabIcon, interactIcon, talkIcon;
    private Image iconDisplay;

    private Color colorNone;
    private Color colorFull;

    void Start()
    {
        textToDisplay = GetComponent<TextMeshProUGUI>();
        iconDisplay = GetComponentInChildren<Image>();
        iconDisplay.sprite = null;
        //unity made me do this...
        colorNone = iconDisplay.color;
        colorFull = iconDisplay.color;
        colorNone.a = 0;
    }

    public void DisplayMessage(ObjectData _objectData)
    {
        switch (_objectData?.InteractionType)
        {
            case InteractionType.isGrabable:
            textToDisplay.text = ("Pick-up " + _objectData.name);
                iconDisplay.sprite = grabIcon;
                iconDisplay.color = colorFull;
                break;
            case InteractionType.isUsable:
            textToDisplay.text = ("Use " + _objectData.name);
                iconDisplay.sprite = interactIcon;
                iconDisplay.color = colorFull;
                break;
            case InteractionType.isNPC:
            textToDisplay.text = ("Talk to " + _objectData.name);
                iconDisplay.sprite = talkIcon;
                iconDisplay.color = colorFull;
                break;
            default:
            textToDisplay.text = " ";
                iconDisplay.sprite = null;
                iconDisplay.color = colorNone;
                break;
        }
    }
    public void CleanMessage()
    {
        textToDisplay.text = " ";
        iconDisplay.sprite = null;
        iconDisplay.color = colorNone;
    }
}
