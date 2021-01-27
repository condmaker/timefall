using UnityEngine;

/// <summary>
/// Type of Interactor that changes its Interation depending on a 
/// setter Object
/// </summary>
public class SwapIteratorInteractor : Interactor
{
    /// <summary>
    /// Instance of a ObjectStateHandler that will be triggered by 
    /// this Interactor
    /// </summary>
    [SerializeField]
    private ObjectStateHandler trigger = null;

    /// <summary>
    /// ObjectStateHandler that changes the type of Iteration this Interactor
    /// uses
    /// </summary>
    [SerializeField]
    private ObjectStateHandler setter = null;

    /// <summary>
    /// List of Iterations this Interactor will change to
    /// The index on the list corresponds to the state on the trigger 
    /// ObjectStateHandler
    /// </summary>
    [SerializeField]
    private Iteration[] iterationList = null;

    /// <summary>
    /// Method is called when the scene starts
    /// </summary>
    private void Awake()
    {
        trigger.OnChangeState += Activate;
    }


    /// <summary>
    /// Method responsible for handling what happens when the trigger
    /// StateHandler changes state
    /// </summary>
    /// <param name="osh">ObjectStateHandler that changed its state</param>
    /// <param name="state">New state of the ObjectStateHandler</param>
    public void Activate(ObjectStateHandler osh, short state)
    {
        short it = (short)Mathf.Clamp(setter.State, 0, iterationList.Length -1);
        iteration = iterationList[it];
        ProcessResult();
    }
}

