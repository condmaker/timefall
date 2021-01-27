using UnityEngine;


/// <summary>
/// Class responsible for storing the data of an Object
/// </summary>
public abstract class ObjectData : ScriptableObject
{
    /// <summary>
    /// Type of interaction the player can have with the object
    /// </summary>
    [SerializeField]
    private InteractionType interactionType;

    /// <summary>
    /// Property that defines the type of interaction the player cans have with
    /// the object
    /// </summary>
    public InteractionType  InteractionType => interactionType;

    /// <summary>
    /// Unique id of the object
    /// </summary> 
    [SerializeField]
    private short iD;

    /// <summary>
    /// Property that defines the unique id of the object
    /// </summary>
    public short ID => iD;

}


