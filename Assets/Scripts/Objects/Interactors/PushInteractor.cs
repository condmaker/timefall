using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteractor : ManualInteractor
{
    //public override event Action OnGoToLast;
    //public override event Action<IterationType> OnGoToNext;
    //public override event Action<short> OnGoTo;

    public override bool Toggle(ItemData itemId, Vector3 position)
    {
        int offset = 2;
        if (position.x > transform.position.x + offset)
            transform.eulerAngles = new Vector3(0,0,0);
        else if (position.x < transform.position.x - offset)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (position.z > transform.position.z + offset)
            transform.eulerAngles = new Vector3(0, -90, 0);
        else if (position.z < transform.position.z - offset)
            transform.eulerAngles = new Vector3(0, 90, 0);

        this.DetectObstacles();

        ProcessResult();

        return false;
    }


    private void DetectObstacles()
    {
        //Raycast
        //Se for Caixa Togller
        //SE for o final da area n mexe ou animação de n mexer
    }
}
