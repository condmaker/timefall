using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of Interactor that expects a specific sequence of ObjectStateHandler
/// to be Activated 
/// </summary>
public class SequenceInteractor : Interactor
{
    /// <summary>
    /// Sequence of ObjectStateHandlers
    /// </summary>
    [SerializeField]
    private List<ObjectStateHandler> wantedStates = null;

    /// <summary>
    /// List of ObjectStateHandlers added by the player
    /// </summary>
    [SerializeField]
    private List<ObjectStateHandler> sequence;

    /// <summary>
    /// Method called when this script is enabled
    /// </summary>
    private void OnEnable()
    {
        foreach (ObjectStateHandler o in wantedStates)
        {
            o.OnChangeState += UpdateState;          
        }
        sequence = new List<ObjectStateHandler>();    
    }

    /// <summary>
    /// Methos responsible for updating the state of the Interactor
    /// </summary>
    /// <param name="osh">ObjectStateHandler that changed its state</param>
    /// <param name="state">New state of the ObjectStateHandler</param>
    private void UpdateState(ObjectStateHandler osh, short state)
    {
        if (state == 0) return;
        sequence.Add(osh);
        CheckCompatibility(osh);
    }

    /// <summary>
    /// Method responsible for checking if the sequence added by the players
    /// is equal with the expected one
    /// </summary>
    /// <param name="osh">The ObjectStateHandler just added to the list</param>
    private void CheckCompatibility(ObjectStateHandler osh)
    {
        bool test = true;

        if (wantedStates.Count != sequence.Count)
            test = false;

        if(test)
            for (int i = 0; i < wantedStates.Count; i++)
            {
                if(wantedStates[i] != sequence?[i])
                {
                    test = false;
                }
            }
        

        if (test)
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

    /// <summary>
    /// Method responsible for clearing the sequence list
    /// </summary>
    public void ResetList()
    {
        for(int i = 0; i < sequence.Count; i++)
        {
            ObjectStateHandler o = sequence[i];
            o.ChangeToState(0);
        }
        sequence.Clear();
    }
}
