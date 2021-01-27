using System;
using UnityEngine;

/// <summary>
/// Class that stores a SateHandler and a state
/// Used in conjunction with an Interactor  
/// </summary>
[Serializable]
public class StateO
{
    [SerializeField]
    private ObjectStateHandler osh = null;
    [SerializeField]
    private short state = 0;
    [SerializeField]
    private bool any = false;

    /// <summary>
    /// Property that stores an instance of an ObjectStateHandler
    /// </summary>
    public ObjectStateHandler Osh => osh;

    /// <summary>
    /// Property that stores the state the ObjectStateHandler needs to be 
    /// </summary>
    public short State => state;

    /// <summary>
    /// Property that defines if the ObjectStateHandler State can be any state
    /// </summary>
    public bool Any => any;
}