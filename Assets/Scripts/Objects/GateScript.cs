using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField]
    private string state = "Status";
    private Animator gateAnimator;

    private void Start()
    {
        gateAnimator = GetComponent<Animator>();
    }
    public void Toggle()
    {
        print("should be changing");
        print(gateAnimator);
        if (gateAnimator.GetBool("Status") == true)
        {
            gateAnimator.SetBool("Status", true);
        }
        else
            gateAnimator.SetBool("Status", false);
    }
}
