using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectToggleList;
    //private List<Animator> objectAnimToggleList;
    //mudar este nome
    private GateScript gate;

    public void Toggle()
    {
        foreach (GameObject gameobject in objectToggleList)
        {
            gate = gameobject.GetComponent<GateScript>();
            gate.Toggle();
        }
        /*
        foreach (Animator animator in objectAnimToggleList)
        {
        if (animator.GetBool(animationVariableName) == true)
            animator.SetBool(animationVariableName, false);
        else
            animator.SetBool(animationVariableName, true);
        }
        */
    }
    
}
