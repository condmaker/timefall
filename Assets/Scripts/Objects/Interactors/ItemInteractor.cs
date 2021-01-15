using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour, IManualInteractor
{
    public event Action OnGoToLast;
    public event Action<IterationType> OnGoToNext;
    public event Action<short> OnGoTo;

    public event Action<ItemData> addData;

    [SerializeField]
    private List<ItemState> unlockers;

    [SerializeField]
    bool NeedAll;

    private ICollection<ItemData> itemsAdded;

    public void Awake()
    {
        itemsAdded = new List<ItemData>();
    }

    public bool Toggle(ItemData itemId)
    {
        if (itemId == null) return false;

        addData?.Invoke(itemId);

        itemsAdded.Clear();
        //Check if there was an update
        foreach(Transform t in transform)
        {
            itemsAdded.Add(
                t.gameObject.GetComponent<DataHolder>().GetData() as ItemData);
        }

        if (IsCombCorrect(unlockers, itemsAdded))
        {
            OnGoTo.Invoke(1);
        }
        else
        {
            //Wrong Weight
            OnGoTo.Invoke(0);
        }
        
        return true;
    }


    public bool IsCombCorrect(IList<ItemState> comb, ICollection<ItemData> toCheck )
    {
        bool result = true;
        int it = 0;

        if (comb.Count != toCheck.Count)
        {
            return false;
        }

        foreach(ItemData iS in toCheck)
        {
            if(!comb[it].Equals(iS))
            {          
                result = false;
                break;
            }

        }

        return result;
    }

}


[Serializable]
public struct ItemState: IEquatable<ItemData>
{
    public short id;
    public short state;

    public bool Equals(ItemData other)
    {
        return id == other.ID;
    }
}
