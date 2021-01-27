using UnityEngine;
using System;

/// <summary>
/// Struct responsible for storing a type of Iteration
/// </summary>
[Serializable]
public struct Iteration
{
    /// <summary>
    /// Variable that defines a type o Iteration
    /// </summary>
    [SerializeField]
    private IterationType iteration; 
    
    /// <summary>
    /// Property that defines a type of Iteration
    /// </summary>
    public IterationType Iter => iteration;

    /// <summary>
    /// Variable that stores a specific state
    /// </summary>
    [SerializeField]
    private short specificState;
    
    /// <summary>
    /// Property that stores a specific state
    /// </summary>
    public short SpecificState => specificState;

    /// <summary>
    /// Variable that defines if this Iteration is using a specific state
    /// to jump to or a type of iteration
    /// </summary>
    [SerializeField]
    private bool specific;

    /// <summary>
    /// Property that defines if this Iteration is using a specific state
    /// to jump to or a type of iteration
    /// </summary>
    public bool Specific => specific;

    /// <summary>
    /// Constructor of the struct Iteration
    /// </summary>
    /// <param name="iter">Iteration Type</param>
    public Iteration(IterationType iter = IterationType.Next)
    {
        iteration = iter;
        specificState = 0;
        specific = false;
    }

}


