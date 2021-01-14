using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour, IManualInteractor
{
    public event Action OnGoToLast;
    public event Action OnGoToNext;
    public event Action<short> OnGoTo;

    public event Action<ItemData> addData;

    [SerializeField]
    private List<ItemState> unlockers;

    [SerializeField]
    bool NeedAll;

    private ICollection<ItemState> itemsAdded;

    public void Awake()
    {
        itemsAdded = new List<ItemState>();
    }

    public bool Toggle(ItemData itemId)
    {
        if (itemId == null) return false;

        addData?.Invoke(itemId);

        foreach(ItemState i in unlockers)
        {
            if(itemId.ID == i.id)
            {
                
                itemsAdded.Add(i);

                if (!NeedAll)
                {
                    OnGoTo.Invoke(i.state);
                }
                else
                {
                    print(1);
                    if (IsCombCorrect(unlockers, itemsAdded))
                    {
                        print(2);
                        OnGoTo.Invoke(1);
                    }
                }
                return true;
            }
        }
        
        return false;
    }


    public bool IsCombCorrect(IList<ItemState> comb, ICollection<ItemState> toCheck )
    {
        bool result = true;
        int it = 0;

        if (comb.Count != toCheck.Count)
        {
            print(comb.Count + " " + toCheck.Count);
            return false;
        }

        foreach(ItemState iS in toCheck)
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
public struct ItemState: IEquatable<ItemState>
{
    public short id;
    public short state;

    public bool Equals(ItemState other)
    {
        return id == other.id;
    }
}
