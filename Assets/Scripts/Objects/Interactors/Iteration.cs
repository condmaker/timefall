using System.Collections;
using UnityEngine;
using System;

[Serializable]
public struct Iteration
{
    [SerializeField]
    private IterationType iteration; 
    public IterationType Iter => iteration;

    [SerializeField]
    private short specificState;
    public short SpecificState => specificState;

    [SerializeField]
    private bool usespecific;
    public bool Usespecific => usespecific;

}
