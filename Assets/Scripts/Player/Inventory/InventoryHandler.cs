using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] slots;

    public ItemData equipedItem => currentSlot?.CurrentItem;

    private InventorySlot currentSlot;

    [SerializeField]
    private GameObject selector;    

    /// <summary>
    /// Adds the given item to an empty slot in the inventory
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

    private void Start()
    {
        selector.SetActive(true);
        currentSlot = slots[0];
    }


    public void EquipItem(InventorySlot slot)
    {   
        currentSlot = slot;
        selector.transform.position = currentSlot.transform.position;

    }

    internal void ClearEquiped()
    {
        currentSlot.CleanDisplay();
    }
}
