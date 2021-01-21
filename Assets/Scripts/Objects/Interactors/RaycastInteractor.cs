using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractor : Interactor
{
    private bool locker;
    // Update is called once per frame
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
