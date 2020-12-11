using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
                
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler        
{

    [SerializeField]
    private InventoryHandler inventory;
    public bool IsEmpty { get; set; }
    [SerializeField]
    private Image itemImage;
    // EDIT by miguel, this was private is now public... 
    //will have to change back later or turn it into a property
    //EDIT 2 this will go back to private later because what needs to happen is a
    //.contains on the inventory and check for the same ObjectData
    //but currently inventory is an array so... later, maybe use for each dunno
    public ItemData CurrentItem { get; private set; }

    public bool isOver;

    private void Update()
    {
        if (isOver)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SelectSlot();

            }
        }
    }

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
    
    public void SelectSlot()
    {
        inventory.EquipItem(this);
        ActivateSlot();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }
}


