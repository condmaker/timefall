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

    public bool Toggle(short? itemId)
    {

        foreach(ItemState i in unlockers)
        {
            if(itemId == i.id)
            {
                OnGoTo.Invoke(i.state);
                return true;
            }
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
