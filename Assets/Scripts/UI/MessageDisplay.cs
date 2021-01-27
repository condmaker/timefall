
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for displaying the interaction icon
/// </summary>
public class MessageDisplay : MonoBehaviour
{
    /// <summary>
    /// Sprites of the diferent types of icon interactions
    /// </summary>
    [SerializeField]
    private Sprite grabIcon  = null,
                interactIcon = null,
                   talkIcon  = null,
                   exitIcon  = null;

    /// <summary>
    /// Image of the icon to display
    /// </summary>
    private Image iconDisplay;

    /// <summary>
    /// Color when there's no item to display
    /// </summary>
    private Color colorNone;

    /// <summary>
    /// Color when there's an item to display
    /// </summary>
    private Color colorFull;


    /// <summary>
    /// Mehtod called before the first frame of the Update
    /// </summary>
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
