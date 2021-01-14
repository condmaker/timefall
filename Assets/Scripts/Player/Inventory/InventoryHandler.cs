using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] slots;


    public ItemData equipedItem => currentSlot.CurrentItem;

 
    private InventorySlot currentSlot;
    [SerializeField]
    private GameObject selector;    

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

    private void Start()
    {
        selector.SetActive(false);
    }


    public void EquipItem(InventorySlot slot)
    {
        selector.SetActive(true);
        
        currentSlot = slot;
        selector.transform.position = currentSlot.transform.position;


        //foreach (InventorySlot iS in slots)
        //{
 
        //    iS.DeativateSlot();
        //}
    }

    internal void ClearEquiped()
    {
        //currentSlot.DeativateSlot();
        currentSlot.CleanDisplay();
    }
}
