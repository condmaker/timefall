using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Data holder that diferenciates what Data to retrieve 
/// depending whether the player has an equipped item or
/// not
/// </summary>
public class MixedDataHolder : DataHolder {
    /// <summary>
    /// List of secondary DataHolders
    /// </summary>
    [SerializeField]
    private List <DataHolder> holderList;

    /// <summary>
    /// Length of holderList's List
    /// </summary>
    private int Count => holderList.Count;


    /// <summary>
    /// Overriden method that defines how
    /// to get the data form the DataHolder
    /// </summary>
    /// <returns>The data contain in this DataHolder</returns>
    public override ObjectData GetData(ItemData equipedItem = null)
    {       
        if (equipedItem != null || holderList.Count < 1)
        {
            //Return when the player has an item equipped 
            return itemData;
        }
        else
        {
            //Return when the player doesn't have an item equipped 
            return holderList[Count - 1].GetData(equipedItem);
        }
    }

    /// <summary>
    /// Adds new secondary DataHolder to holderList
    /// </summary>
    /// <param name="dH">Data Holder to add</param>
    public void AddData(DataHolder dH)
    {
        holderList.Add(dH);
    }


    /// <summary>
    /// Overriden method that defines what to do
    /// when the item is Destroyed
    /// </summary>
    internal override void DestroyObject()
    {
        holderList[Count - 1].DestroyObject();
        holderList.RemoveAt(Count - 1);
    }

}
