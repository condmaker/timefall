using UnityEngine;

/// <summary>
/// Type of Interactor tha is Activated when an Object enters the Raycast 
/// </summary>
public class RaycastInteractor : Interactor
{
    /// <summary>
    /// Auxiliary var to make sure the Interactor doesn't have repeat
    /// interactions
    /// </summary>
    private bool locker;
    
    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update()
    {
        RaycastHit currentWorldObject;
        //Raycast
        bool hit = Physics.Raycast(
               transform.position, transform.forward,
               out currentWorldObject, 2);

        if(hit && !locker)
        {
            ProcessResult();
            locker = true;
        }
        else if(!hit && locker)
        {
            ProcessResult(0);
            locker = false;
        }
    
    
    }
}
