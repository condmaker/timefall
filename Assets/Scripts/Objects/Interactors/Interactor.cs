﻿using System;
using UnityEngine;

[RequireComponent(typeof(ObjectStateHandler))]
public abstract class Interactor: MonoBehaviour
{
    public event Action OnGoToLast;
    public event Action<IterationType> OnGoToNext;
    public event Action<short> OnGoTo;

    [SerializeField]
    protected Iteration iteration;
    public Iteration Iteration => iteration;

    protected void ProcessResult(short? state = null, IterationType? iter = null)
    {
        if(state != null)
        {
            OnGoTo?.Invoke(state ?? 0);
            return;
        }

        if(iter != null)
        {
            RunIteration(iter ?? IterationType.Next);
            return;
        }

        if (iteration.Specific)
        {
            OnGoTo?.Invoke(iteration.SpecificState);           
        }
        else
        {
            RunIteration(Iteration.Iter);
        }
       
    }

    private void RunIteration(IterationType iter)
    {

        switch (iter)
        {
            case IterationType.Last:
                OnGoToLast?.Invoke();
                break;
            case IterationType.Next:
                OnGoToNext?.Invoke(iteration.Iter);
                break;
            case IterationType.Previous:
                OnGoToNext?.Invoke(iteration.Iter);
                break;

        }
    }
}
