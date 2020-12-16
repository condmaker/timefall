using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglerInteractor : MonoBehaviour, IInteractor
{
    public event Action OnGoToLast;
    public event Action OnGoToNext;
    public event Action<short> OnGoTo;


    private bool wasTriggered;

    [SerializeField]
    private List<StateO> wantedStates;

    private Dictionary<ObjectStateHandler, short> currentStates;

    private void OnEnable()
    {
        currentStates = new Dictionary<ObjectStateHandler, short>();

        //currentStates = new
        foreach (StateO o in wantedStates)
        {
            o.osh.OnChangeState += UpdateState;
            currentStates.Add(o.osh, 0);         
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
            if (osh == o.osh)
            {
                if (o.any)
                {
                    OnGoToNext.Invoke();
                    return;
                }
            }


            if (o.state != currentStates[o.osh])
            {
               
                compatible = false;
            }
        }

        if (compatible) 
        {
            OnGoToLast?.Invoke();
            wasTriggered = true;
        }
        else
        {
            if (wasTriggered)
            {
                OnGoTo?.Invoke(0);
                wasTriggered = false;
            }
        }
    }
}


[Serializable]
public class StateO
{

    //Precisam de ser private e ter propriedades 
    public ObjectStateHandler osh;
    public short state;
    public bool any;

}



