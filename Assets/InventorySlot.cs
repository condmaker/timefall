using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public bool IsEmpty { get; set; }
    [SerializeField]
    private Image itemImage;
    private ObjectData currentItem;    

    /// <summary>
    /// Displays the given item in the invetory slot
    /// </summary>
    /// <param name="item">ScriptableObject that contains the item data</param>
    public void DisplayItem(ObjectData item)
    {
        currentItem = item;
        itemImage.sprite = item.inventoryImage;
        IsEmpty = true;
    }


    /// <summary>
    /// Cleans the invetory slot
    /// </summary>
    public void CleanDisplay()
    {
        currentItem = null;
        itemImage.sprite = null;
        IsEmpty = false;
    }
    
}
