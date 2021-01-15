using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ManualInteractor : MonoBehaviour, IManualInteractor
{

    [SerializeField]
    private short unlockerId;
    
    [SerializeField]
    private bool needsItem;

    //Eu acho q tem de ser public. 
    public event Action OnGoToLast;
    public event Action OnGoToNext;
    public event Action<short> OnGoTo;


    public bool Toggle(ItemData itemId)
    {

        if (unlockerId == itemId?.ID || !needsItem)
        {
            OnGoToNext?.Invoke();
            if (needsItem)
            {
                return true;
            }
        }
        return false;
    }
}

