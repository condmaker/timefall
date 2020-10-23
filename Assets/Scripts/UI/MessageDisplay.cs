using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private TextMeshProUGUI textToDisplay;

    void Start()
    {
        textToDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayMessage(ObjectData _objectData)
    {
        switch (_objectData.InteractionType)
        {
            case InteractionType.isGrabable:
            textToDisplay.text = ("pick-up " + _objectData.name);
                break;
            case InteractionType.isUsable:
            textToDisplay.text = ("Use " + _objectData.name);
                break;
            case InteractionType.isNPC:
            textToDisplay.text = ("Talk to " + _objectData.name);
                break;
            default:
            textToDisplay.text = " ";
                break;
        }
    }
    public void CleanMessage()
    {
        textToDisplay.text = " ";
    }
}
