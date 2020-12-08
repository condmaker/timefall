using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayHandler : MonoBehaviour
{
    [SerializeField]
    private Text dialogueDisplayTarget;
    
    // Depois maybe tirar o serializable
    [SerializeField]
    private DialogueScript currentScript;

    public NodeData welp;

    private string currentGUID;
    private string currentLine;

    // Start is called before the first frame update
    void Start()
    {
        welp = currentScript.GetNodeByIndex(0);
        currentLine = currentScript.GetNodeByIndex(0).Dialogue;
        currentGUID = currentScript.GetNodeByIndex(0).GUID;
        DisplayLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextLine();
    }

    private void NextLine()
    {
        NodeData currentData = currentScript.GetNodeByGUID(currentGUID);
        
        if (currentData.OutPorts.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentGUID = currentData.OutPorts[0];
        NodeData nextData = currentScript.GetNodeByGUID(currentGUID);
        currentLine = nextData.Dialogue;

        DisplayLine();

    }

    private void EndDialogue()
    {
        dialogueDisplayTarget.text = "";
    }

    private void DisplayLine()
    {
        dialogueDisplayTarget.text = currentLine;
    }
}
