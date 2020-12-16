using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour, IInteractor
{
    //Eu acho q tem de ser public. 
    public event Action OnGoToLast;
    public event Action OnGoToNext;
    public event Action<short> OnGoTo;

    [SerializeField]
    private List<ItemState> unlockers;

    public void Toggle(short? itemId)
    {

        foreach(ItemState i in unlockers)
        {
            if(itemId == i.id)
            {
                OnGoTo.Invoke(i.state);
            }
        }
    }

}


[Serializable]
public class ItemState
{
    public int id;
    public short state;
}
