using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteractor : ManualInteractor
{
    
    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        bool aux;
        return Push(position, out aux);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.right * 5, Color.red);
    }

    public InteractionResult Push(Vector3 position, out bool result)
    {
        int offset = 2;
        if (position.x > transform.position.x + offset)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (position.x < transform.position.x - offset)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (position.z > transform.position.z + offset)
            transform.eulerAngles = new Vector3(0, -90, 0);
        else if (position.z < transform.position.z - offset)
            transform.eulerAngles = new Vector3(0, 90, 0);

        result = DetectObstacles();
        if (result)
        {
            ProcessResult();
        }

        return InteractionResult.Activate;
    }

    public bool DetectObstacles()
    {
        RaycastHit currentWorldObject;
        //Raycast
         bool hit = Physics.Raycast(
                transform.position, -transform.right, 
                out currentWorldObject, 6);


        GameObject nearObject = hit ? currentWorldObject.collider.gameObject: null;
        
        //Se for Caixa Togller
        if (nearObject?.name.Contains("Box") ?? false)
        {
            PushInteractor pushInteractor 
                = nearObject.GetComponent<PushInteractor>();

            bool result = false;
            pushInteractor.Push(this.transform.position, out result);
            return result;       
        }
        else if (!hit)
        {          
            return true;
        }
        else
        {
            //SE for o final da area n mexe ou animação de n mexer
            return false;
        }

    }
}
