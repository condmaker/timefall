using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectToggleList;
    private List<Animator> objectAnimToggleList;
    [SerializeField]
    private string animationVariableName = "Status";

    private void Awake()
    {
        MakeList();
        print(objectAnimToggleList);
    }
    public void Toggle()
    {
        foreach (Animator animator in objectAnimToggleList)
        {
        if (animator.GetBool(animationVariableName) == true)
            animator.SetBool(animationVariableName, false);
        else
            animator.SetBool(animationVariableName, true);
        }
    }
    private void MakeList()
    {
        foreach (GameObject gameobject in objectToggleList)
        {
            objectAnimToggleList.Add(gameobject.GetComponent<Animator>());
        }
    }
}
