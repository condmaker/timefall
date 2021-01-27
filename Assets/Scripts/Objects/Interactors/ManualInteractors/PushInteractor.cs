using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Type of ManualInteractor that takes into account the current position
///of the player and rotates the object accordingly
/// </summary
public class PushInteractor : ManualInteractor
{
    //// <summary>
    /// Method responsible for handling what happens when the player interacts
    /// with this Interactor
    /// 
    /// This method initiates the Push action of the object and returns 
    /// its value
    /// </summary>
    /// <param name="itemId">Data of the item currently equipped</param>
    /// <param name="position">Current Position of the player</param>
    /// <returns>InteractionResult based on the evaluation</returns>
    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        bool aux;
        return Push(position, out aux);
    }


    /// <summary>
    /// Method responsible for rotating the object to the appropriate position 
    /// </summary>
    /// <param name="position">Current Position of the player</param>
    /// <param name="result">Bool that defines if the action
    /// was performed</param>
    /// <returns>InteractionResult based on the evaluation</returns>
    public InteractionResult Push(Vector3 position, out bool result)
    {

        int offset = 2;
        
        //Rotate the object
        if (position.x > transform.position.x + offset)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (position.x < transform.position.x - offset)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (position.z > transform.position.z + offset)
            transform.eulerAngles = new Vector3(0, -90, 0);
        else if (position.z < transform.position.z - offset)
            transform.eulerAngles = new Vector3(0, 90, 0);

        //Check if there's any obstacles in front of the object
        result = DetectObstacles();
        
        if (result)
        {
            ProcessResult();
        }
        return InteractionResult.Activate;
    }


    /// <summary>
    /// Method reponsible for detecting if there's any obstacles in front
    /// of the object
    /// </summary>
    /// <returns></returns>
    private bool DetectObstacles()
    {
        RaycastHit currentWorldObject;

        //Raycast to check of there's any obstacles
        bool hit = Physics.Raycast(
                transform.position, -transform.right, 
                out currentWorldObject, 6);

        //Obstacle in front of the object
        GameObject nearObject = 
            hit ? currentWorldObject.collider.gameObject: null;
        
        //Check the type of obstacle
        if (nearObject?.name.Contains("Box") ?? false)
        {
            //Get the PushInteractor component of the obstacle
            PushInteractor pushInteractor 
                = nearObject.GetComponent<PushInteractor>();
        
            bool result = false;
           
            //Push obstacle if it is a Box object
            pushInteractor.Push(this.transform.position, out result);
            
            return result;       
        }
        else if (!hit)
        {          
            return true;
        }
        else
        {
            return false;
        }

    }
}
