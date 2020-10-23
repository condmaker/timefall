using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                s.DisplayItem(item);
                break;
            }
        }
    }

    public void DropItem()
    {

    }
}
