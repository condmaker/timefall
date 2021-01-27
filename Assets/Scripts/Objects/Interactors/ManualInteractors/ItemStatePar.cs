using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for storing a  par of one item id and one state
/// </summary>
[System.Serializable]
public class ItemStatePar 
{

   
    [SerializeField]
    private short id = 0;

    /// <summary>
    /// Property that defines an item ID
    /// </summary>
    public short ID => id;


    [SerializeField]
    private short state = 0;

    /// <summary>
    // Property that defines a state
    /// </summary>
    public short State => state;
}
