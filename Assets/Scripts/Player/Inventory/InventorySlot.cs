using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
              

/// <summary>
/// Class responsible for controlling a Slot of the Inventory
/// </summary>
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Variable that stores an instance of the inventory
    /// </summary>
    [SerializeField]
    private InventoryHandler inventory;

    /// <summary>
    /// Property that defines if the slot is has an item or not
    /// </summary>
    public bool IsEmpty { get; set; }

    /// <summary>
    /// Image of the current item
    /// </summary>
    [SerializeField]
    private Image itemImage;

    /// <summary>
    /// Property that defines the data related to the current item
    /// </summary>
    public ItemData CurrentItem { get; private set; }

    /// <summary>
    /// Variable that defines the color of the slot
    /// when it is empty
    /// </summary>
    private Color colorNone;

    /// <summary>
    /// Variable that defines the color of the slot
    /// when it has an item
    /// </summary>
    private Color colorFull;

    /// <summary>
    /// Id unique to each slot.
    /// This id defines which key will select
    /// the slot
    /// </summary>
    [SerializeField]
    private short id;

    /// <summary>
    /// Update method of the class
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown((KeyCode) (id+48)))
        {
            inventory.EquipItem(this);
        }
    }


    /// <summary>
    /// Method responsible for setting up the 
    /// starting parameters of the class
    /// </summary>
    void Start()
    {
        colorNone = itemImage.color;
        colorFull = itemImage.color;
        colorFull.a = 100;
    }

    /// <summary>
    /// Displays the given item in the invetory slot
    /// </summary>
    /// <param name="item">ScriptableObject that contains the item data</param>
    public void DisplayItem(ItemData item)
    {
        CurrentItem = item;
        itemImage.sprite = item.UIobjectSprite;
        itemImage.color = colorFull;
        IsEmpty = true;
    }

    /// <summary>
    /// Cleans the invetory slot
    /// </summary>
    public void CleanDisplay()
    {
        CurrentItem = null;
        itemImage.sprite = null;
        itemImage.color = colorNone;
        IsEmpty = false;
    }


    /// <summary>
    /// Method responsible for handling what happens
    /// when the user clicks a GameObject with this script
    /// attached
    /// </summary>
    /// <param name="eventData">Current PointerEventData</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.EquipItem(this);
    }
}


