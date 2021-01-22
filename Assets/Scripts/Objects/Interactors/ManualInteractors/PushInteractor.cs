using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteractor : ManualInteractor
{
    
    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        return Push(position);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.right * 5, Color.red);
    }

    public InteractionResult Push(Vector3 position)
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

        if (DetectObstacles())
        {
            ProcessResult();
        }

        return InteractionResult.Activate;
    }

    private bool DetectObstacles()
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
            nearObject.GetComponent<PushInteractor>().Push(this.transform.position);
            return true;
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
