using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class HandInteractor : ManualInteractor
{

    [SerializeField]
    private short unlockerId;
    
    [SerializeField]
    private bool needsItem;

    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {

        if (unlockerId == itemId?.ID || !needsItem)
        {
            ProcessResult();
            if(!needsItem)
                return InteractionResult.Activate;
            return InteractionResult.UseItem;
        }

        return InteractionResult.WrongIntMessage;
    }

}

