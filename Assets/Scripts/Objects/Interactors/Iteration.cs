using System.Collections;
using UnityEngine;
using System;
using UnityEditor;

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
    private bool specific;
    public bool Specific => specific;

}


