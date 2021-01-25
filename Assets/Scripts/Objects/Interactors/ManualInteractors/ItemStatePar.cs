using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for storing a 
/// par of one item id and one state
/// </summary>
[System.Serializable]
public class ItemStatePar 
{

    /// <summary>
    /// Property that defines an item ID
    /// </summary>
    [SerializeField]
    private short id;
    public short ID => id;


    /// <summary>
    // Property that defines a state
    /// </summary>
    [SerializeField]
    private short state;
    public short State => state;
}
