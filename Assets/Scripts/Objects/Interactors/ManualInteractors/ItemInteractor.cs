using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : ManualInteractor
{

    public event Action<ItemData> addData;

    [SerializeField]
    private List<short> unlockers;

    [SerializeField]
    bool NeedAll;

    private ICollection<ItemData> itemsAdded;

    public void Awake()
    {
        itemsAdded = new List<ItemData>();
    }

    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        if (itemId == null) 
            return InteractionResult.WrongIntMessage;

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
            ProcessResult();
        }
        else
        {
            //Wrong Weight
            ProcessResult(0);
        }
        
        return InteractionResult.UseItem;
    }


    public bool IsCombCorrect(List<short> comb, ICollection<ItemData> toCheck )
    {
        bool result = true;
        int it = 0;

        if (comb.Count != toCheck.Count)
        {
            return false;
        }

        foreach(ItemData iS in toCheck)
        {
            if(!(comb[it] == iS.ID))
            {
                result = false;
                break;
            }
            it++;
        }

        return result;
    }


}

