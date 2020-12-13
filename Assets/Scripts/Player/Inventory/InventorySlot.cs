using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
                
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private InventoryHandler inventory;
    public bool IsEmpty { get; set; }
    [SerializeField]
    private Image itemImage;
    public ItemData CurrentItem { get; private set; }

    /// <summary>
    /// Displays the given item in the invetory slot
    /// </summary>
    /// <param name="item">ScriptableObject that contains the item data</param>
    public void DisplayItem(ItemData item)
    {
        CurrentItem = item;
        itemImage.sprite = item.UIobjectSprite;
        IsEmpty = true;
    }

    public void ActivateSlot()
    {
        GetComponent<Image>().color = Color.black;
    }
    public void DeativateSlot()
    {
        GetComponent<Image>().color = Color.white;
    }

    /// <summary>
    /// Cleans the invetory slot
    /// </summary>
    public void CleanDisplay()
    {
        CurrentItem = null;
        itemImage.sprite = null;
        IsEmpty = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.EquipItem(this);
        ActivateSlot();
    }
}


