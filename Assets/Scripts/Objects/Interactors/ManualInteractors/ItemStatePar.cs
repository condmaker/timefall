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
    /// Item id
    /// </summary>
    [SerializeField]
    private short id;
    public short ID => id;


    /// <summary>
    /// Wanted State
    /// </summary>
    [SerializeField]
    private short state;
    public short State => state;
}
