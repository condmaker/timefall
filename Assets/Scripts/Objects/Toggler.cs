using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be more global, since this will only work with gates. Maybe make
// GateScript inherit from InteractibleScript and then make BridgeScript and
// FireScript inherit from that too
public class Toggler : MonoBehaviour
{
    // Maybe a list isn't that efficient since it will be static
    [SerializeField]
    private List<GameObject> objectToggleList;
    //mudar este nome
    private GateScript gate;

    public void Toggle()
    {
        foreach (GameObject gameobject in objectToggleList)
        {
            gate = gameobject.GetComponent<GateScript>();
            gate.Toggle();
        }
    }
    
}
