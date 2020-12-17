using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour, IManualInteractor
{
    //Eu acho q tem de ser public. 
    public event Action OnGoToLast;
    public event Action OnGoToNext;
    public event Action<short> OnGoTo;

    [SerializeField]
    private List<ItemState> unlockers;

    [SerializeField]
    bool NeedAll;
    [SerializeField]
    int itemsAdded;



    public bool Toggle(short? itemId)
    {        
        foreach(ItemState i in unlockers)
        {
            if(itemId == i.id)
            {
                itemsAdded++;
                if (!NeedAll)
                {
                    OnGoTo.Invoke(i.state);
                }
                else
                {
                    if (itemsAdded == unlockers.Count)
                    {
                        OnGoTo.Invoke(1);
                    }
                }
                return true;
            }
        }

        if (NeedAll)
        {
           
        }

        return false;
    }
}


[Serializable]
public class ItemState
{
    public short id;
    public short state;
}
