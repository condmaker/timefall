using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapIteratorInteractor : Interactor
{
    [SerializeField]
    private ObjectStateHandler trigger;
    [SerializeField]
    private ObjectStateHandler setter;
    [SerializeField]
    private Iteration[] iterationList;

    private void Awake()
    {
        trigger.OnChangeState += Activate;
    }

    public void Activate(ObjectStateHandler osh, short state)
    {

        short it = (short)Mathf.Clamp(setter.State, 0, iterationList.Length -1);
        iteration = iterationList[it];
        ProcessResult();
    }
}

