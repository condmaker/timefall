using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteractor : MonoBehaviour, IManualInteractor
{
    public event Action OnGoToLast;
    public event Action<IterationType> OnGoToNext;
    public event Action<short> OnGoTo;

    public bool Toggle(ItemData itemId, Vector3 position)
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

        OnGoTo?.Invoke(1);
        
        return false;
    }

    private void DetectObstacles()
    {
        //Raycast
        //Se for Caixa Togller
        //SE for o final da area n mexe ou animação de n mexer
    }
}
