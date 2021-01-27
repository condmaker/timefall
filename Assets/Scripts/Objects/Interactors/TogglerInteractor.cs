using System.Collections.Generic;
using UnityEngine;

public class TogglerInteractor : Interactor
{
    private bool wasTriggered;

    [SerializeField]
    private List<StateO> wantedStates;
   
    /// <summary>
    /// Variable that defines if the state returns to 0
    /// if the conditions are not met or if it persists
    /// </summary>
    [SerializeField]
    private bool persistance;


    private Dictionary<ObjectStateHandler, short> currentStates;

    /// <summary>
    /// Method is called when the scene starts
    /// </summary>
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

    /// <summary>
    /// Method responsible for handling what happens when one of the 
    /// StateHandlers associated with this Interactor changes states
    /// </summary>
    /// <param name="osh">ObjectStateHandler that changed its state</param>
    /// <param name="state">New state of the ObjectStateHandler</param>
    private void UpdateState(ObjectStateHandler osh, short state)
    {
        if (currentStates.ContainsKey(osh))
        {
            currentStates[osh] = state;
        }
        CheckCompatibility(osh);
    }


    /// <summary>
    /// Check if the all ObjectStateHandlers in wantedStates have the correct state 
    /// </summary>
    /// <param name="osh">ObjectStateHandler that changed its state</param>
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
                if (!persistance)
                    ProcessResult(0);

                wasTriggered = false;
            }
        }
    }
}






