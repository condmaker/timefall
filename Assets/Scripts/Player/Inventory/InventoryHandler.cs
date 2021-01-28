using UnityEngine;

/// <summary>
/// Class responsible for handling the Inventory system
/// </summary>
public class InventoryHandler : MonoBehaviour
{
    /// <summary>
    /// List of all InventorySlots that make up the Inventory
    /// </summary>
    [SerializeField]
    private InventorySlot[] slots = default;

    /// <summary>
    /// Data of the item currently equipped by the player
    /// </summary>
    public ItemData equipedItem => currentSlot?.CurrentItem;

    /// <summary>
    /// Slot currenty equipped by the player
    /// </summary>
    private InventorySlot currentSlot;

    /// <summary>
    /// Selector component of the inventory
    /// </summary>
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


   
    /// <summary>
    /// Method responsible for setting up the 
    /// starting parameters of the class
    /// </summary>
    private void Start()
    {
        selector.SetActive(true);
        currentSlot = slots[0];
    }


    /// <summary>
    /// Method responsible for selecting what 
    /// slot is currently equipped
    /// </summary>
    /// <param name="slot">The slot to be equiped</param>
    public void EquipItem(InventorySlot slot)
    {   
        currentSlot = slot;
        selector.transform.position = currentSlot.transform.position;

    }

    /// <summary>
    /// Method responsible for clearing the selected slot
    /// </summary>
    internal void ClearEquiped()
    {
        currentSlot.CleanDisplay();
    }
}
