using System;
using UnityEngine;

/// <summary>
/// Class responsible for defining a way to interact with Object State Handlers
/// </summary>
[RequireComponent(typeof(ObjectStateHandler))]
public abstract class Interactor: MonoBehaviour
{
    /// <summary>
    /// Event triggered when the condition defined by the Interactor are true
    /// This event is selected when the iteration is setup to iterate to
    /// the last state
    /// </summary>
    public event Action OnGoToLast;

    /// <summary>
    /// Event triggered when the condition defined by the Interactor are true
    /// This event is selected when the iteration is setup to iterate to
    /// the next state in any direction
    /// </summary>
    public event Action<IterationType> OnGoToNext;

    /// <summary>
    /// Event triggered when the condition defined by the Interactor are true
    /// This event is selected when the iteration is setup to iterate to
    /// a specific state
    /// </summary>
    public event Action<short> OnGoTo;

    /// <summary>
    /// Type of iteration that defines the Interactor
    /// This iteration will decide what event to trigger
    /// </summary>
    [SerializeField]
    protected Iteration iteration;

    /// <summary>
    /// Property that defines Type of iteration that defines the Interactor
    /// </summary>
    public Iteration Iteration => iteration;


    /// <summary>
    /// Method responsible for deciding what event to trigger based on 
    /// the Iteration parameters
    /// </summary>
    /// <param name="state">Specific state to iterate to</param>
    /// <param name="iter">Iteration type to iterate to</param>
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


    /// <summary>
    /// Method responsible for invoking the specific event 
    /// based on the IterationType
    /// </summary>
    /// <param name="iter">Iteration type to iterate to</param>
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

