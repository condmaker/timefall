using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectToggler : Interactor
{


    [SerializeField]
    private List<StateO> wantedStates;

    private void Awake()
    {
        foreach (StateO o in wantedStates)
        {
            o.Osh.OnChangeState += UpdateState;
        }
    }

    private void UpdateState(ObjectStateHandler osh, short state)
    {
        CheckCompatibility(osh);
    }

    private void CheckCompatibility(ObjectStateHandler osh)
    {
        short it = 0;

        foreach (StateO o in wantedStates)
        {
            if (o.Osh.State >= 1)
            {
                ProcessResult(o.State);
                it++;
            }
        }
    }
}




