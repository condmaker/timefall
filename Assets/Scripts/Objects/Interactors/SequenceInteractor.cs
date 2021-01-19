﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceInteractor : Interactor
{
    private bool wasTriggered;

    [SerializeField]
    private List<ObjectStateHandler> wantedStates;

    [SerializeField]
    private List<ObjectStateHandler> sequence;

    private void OnEnable()
    {
        foreach (ObjectStateHandler o in wantedStates)
        {
            o.OnChangeState += UpdateState;          
        }
        sequence = new List<ObjectStateHandler>();    
    }

    private void UpdateState(ObjectStateHandler osh, short state)
    {
        sequence.Add(osh);
        CheckCompatibility(osh);
    }

    private void CheckCompatibility(ObjectStateHandler osh)
    {
        bool test = true;

        if (wantedStates.Count != sequence.Count)
            test = false;

        for (int i = 0; i < wantedStates.Count; i++)
        {
            if(wantedStates[i] != sequence?[i])
            {
                test = false;
            }
        }

        if(test)
        {
            ProcessResult();
        }
        else
        {
            if(wantedStates.Count == sequence.Count)
            {
                ResetList();
            }
        }

    }

    public void ResetList()
    {
        foreach (ObjectStateHandler o in sequence)
        {
            o.ChangeToState(0);
        }
        sequence.Clear();
    }
}
