using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class ChangeDialogue : MonoBehaviour
{
    private ObjectStateHandler osh;

    [SerializeField]
    private int state;

    [SerializeField]
    private NpcData npc;

    [SerializeField]
    private DialogueScript script;

    // Start is called before the first frame update
    void Start()
    {
        osh = GetComponent<ObjectStateHandler>();
        if (osh != null)
            osh.OnChangeState += Change;
    }

    public void Change(ObjectStateHandler oSH, short state)
    {
        if(osh.State == state)
            npc.Dialogue = script;
    }
}
