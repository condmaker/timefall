using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectToggler : MonoBehaviour, IInteractor
{


    [SerializeField]
    private List<StateO> wantedStates;


    public event Action OnGoToLast;
    public event Action OnGoToNext;
    public event Action<short> OnGoTo;

    private void Awake()
    {
        
        foreach (StateO o in wantedStates)
        {
            o.osh.OnChangeState += UpdateState;
        }
    }

    private void UpdateState(ObjectStateHandler osh, short state)
    {
        CheckCompatibility(osh);
    }

    private void CheckCompatibility(ObjectStateHandler osh)
    {
        foreach (StateO o in wantedStates)
        {
            if (o.osh.State >= 1)
            {
                OnGoTo?.Invoke(o.state);
            }
        }
    }
}




