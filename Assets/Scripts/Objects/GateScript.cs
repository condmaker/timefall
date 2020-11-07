using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is very strange and breaks encapsulation. We should make it inherit
// ObjectData or something that signals that the gate is an World Object.
public class GateScript : MonoBehaviour
{
    private Animator gateAnimator;
    // This is inefficient, we should change it to ID later
    private const string state = "Status";

    private void Start()
    {
        gateAnimator = GetComponent<Animator>();
    }
    public void Toggle()
    {
        if (gateAnimator.GetBool(state) == false)
            gateAnimator.SetBool(state, true);
        else
            gateAnimator.SetBool(state, false);
    }
}
