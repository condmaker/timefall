using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of Interactor that directly jumps to a specific state when the 
/// corresponding StateHandler changes states
/// </summary>
public class DirectToggler : Interactor
{

    /// <summary>
    /// List of StateHandlers and the state to activate when 
    /// the respective StateHandler changes states
    /// </summary>
    [SerializeField]
    private List<StateO> wantedStates = null;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    private void Awake()
    {
        foreach (StateO o in wantedStates)
        {
            o.Osh.OnChangeState += UpdateState;
        }
    }

    /// <summary>
    /// Method responsible for handling what happens when one of
    /// StateHandlers in the list changes states
    /// </summary>
    /// <param name="osh">ObjectStateHandler that changed its state</param>
    /// <param name="state">New state of the ObjectStateHandler</param>
    private void UpdateState(ObjectStateHandler osh, short state)
    {
        CheckCompatibility(osh);
    }

    /// <summary>
    /// Method responsible for checking if the the ObjectStateHandler chanaged 
    /// states to the appropriate state
    /// </summary>
    /// <param name="osh">ObjectStateHandler that just changed states</param>
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




