using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is very strange and breaks encapsulation. We should make it inherit
// ObjectData or something that signals that the gate is an World Object.
public class OpenObject : MonoBehaviour
{
    private Animator ObjectAnimator;
    // This is inefficient, we should change it to ID later
    private const string state = "Status";

    private void Start()
    {
        ObjectAnimator = GetComponent<Animator>();
    }
    public void Toggle()
    {
        if (ObjectAnimator.GetBool(state) == false)
            ObjectAnimator.SetBool(state, true);
        else
            ObjectAnimator.SetBool(state, false);
    }
}
