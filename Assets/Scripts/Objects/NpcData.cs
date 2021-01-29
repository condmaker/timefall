using UnityEngine;
using DialogueSystem;

/// <summary>
/// Class responsible for storing the data of an NPC
/// </summary>
[CreateAssetMenu(fileName = "Data Asset", menuName = "ScriptableObjects/Object Data/NPC Data")]
public class NpcData : ObjectData
{
    /// <summary>
    /// Dialogue script of the NPC
    /// </summary>
    [SerializeField]
    private DialogueScript dialogue;
    
    /// <summary>
    /// Property that defines the dialogue script of the NPC 
    /// </summary>
    public DialogueScript Dialogue 
    { 
        get { return dialogue; }
        set { dialogue = value; } }

}
