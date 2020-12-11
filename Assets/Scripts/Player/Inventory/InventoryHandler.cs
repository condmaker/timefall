using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] slots;
    
    [SerializeField]
    public ItemData equipedItem { get; private set; }

    /// <summary>
    /// Adds the given item to an empty slot in the invetory
    /// </summary>
    /// <param name="item">ScriptableObject that contains the item data</param>
    public void AddItem(ItemData item)
    {
        foreach (InventorySlot s in slots)
        {
            if (!s.IsEmpty)
            {
                s.DisplayItem(item);
                break;
            }
        }
    }

    private void Update()
    {

    }


    public void EquipItem(InventorySlot slot)
    {
        equipedItem = slot.CurrentItem;      
        foreach(InventorySlot iS in slots)
        {
            iS.DeativateSlot();
        }
    }

    public void DropItem()
    {

    }
}
