using UnityEngine;

/// <summary>
/// Class reponsible for managing the Inventory System
/// </summary>
public class InventoryScript : MonoBehaviour
{
    /// <summary>
    /// List of slots of the inventory
    /// </summary>
    [SerializeField]
    private InventorySlot[] slots = null;

    /// <summary>
    /// Adds the given item to an empty slot in the invetory
    /// </summary>
    /// <param name="item">ScriptableObject that contains the item data</param>
    public void AddItem(ObjectData item)
    {
        foreach (InventorySlot s in slots)
        {
            if (!s.IsEmpty)
            {
                s.DisplayItem(item as ItemData);
            }
        }
    }

}
