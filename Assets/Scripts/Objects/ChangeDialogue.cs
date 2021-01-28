using UnityEngine;
using DialogueSystem;

/// <summary>
/// Class responsible for changing the dialogue script of an NPC when an
/// object changes states
/// </summary>
public class ChangeDialogue : MonoBehaviour
{
    /// <summary>
    /// Instance of the ObjectStateHandler of this object
    /// </summary>
    private ObjectStateHandler osh;

    /// <summary>
    /// State the object needs to be to change the npc dialogue
    /// </summary>
    [SerializeField]
    private int state;

    /// <summary>
    /// Npc to change the dialogue from
    /// </summary>
    [SerializeField]
    private NpcData npc;

    /// <summary>
    /// New dialogue to assign to the NPC
    /// </summary>
    [SerializeField]
    private DialogueScript script;

    

    /// <summary>
    /// Method called before the first frame update
    /// </summary>
    void Start()
    {
        osh = GetComponent<ObjectStateHandler>();
        if (osh != null)
            osh.OnChangeState += Change;
    }

    /// <summary>
    /// Method responsible for changing the dialogue of the npc
    /// </summary>
    /// <param name="oSH">ObjectStateHandler that just changed states</param>
    /// <param name="state">State the StateHandler changed to</param>
    public void Change(ObjectStateHandler oSH, short state)
    {

        if (state == this.state) 
        {
            npc.Dialogue = script;          
        }


    }
}
