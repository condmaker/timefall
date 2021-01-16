using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerInteractor : Interactor
{
    private bool wasTriggered;

    [SerializeField]
    private List<StateO> wantedStates;

    private Dictionary<ObjectStateHandler, short> currentStates;

    private void Awake()
    {
        currentStates = new Dictionary<ObjectStateHandler, short>();

        //currentStates = new
        foreach (StateO o in wantedStates)
        {
            o.Osh.OnChangeState += UpdateState;
            currentStates.Add(o.Osh, 0);         
        }
    }

    private void UpdateState(ObjectStateHandler osh, short state)
    {
        if (currentStates.ContainsKey(osh))
        {
            currentStates[osh] = state;
        }
        CheckCompatibility(osh);
    }

    private void CheckCompatibility(ObjectStateHandler osh)
    {
        bool compatible = true;
        foreach (StateO o in wantedStates)
        {

            //Scuffed
            if (osh == o.Osh)
            {
                if (o.Any)
                {
                    ProcessResult();
                    return;
                }
            }


            if (o.State != currentStates[o.Osh])
            {
                compatible = false;
            }
        }

        if (compatible) 
        {
            ProcessResult();
            wasTriggered = true;
        }
        else
        {
            if (wasTriggered)
            {
                ProcessResult(0);
                wasTriggered = false;
            }
        }
    }
}






