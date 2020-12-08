using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueScript : ScriptableObject
{
    [SerializeField]
    public string DialogueName;
    private int dialogueID;

    private List<InputOutputData<string, NodeData>> dialogueNodes =
        new List<InputOutputData<string, NodeData>>();

    public int Count => dialogueNodes.Count; 

    public void FillDialogueDic(NodeData nd)
    {
        InputOutputData<string, NodeData> par = new InputOutputData<string, NodeData>(nd.GUID, nd);
        dialogueNodes.Add(par);
    }

    public NodeData GetNodeByIndex(int index)
    {
        return dialogueNodes[index].Value;
    }

    //Gets the node using its id
    //This option is less efficient 
    public NodeData GetNodeByGUID(string id)
    {
        foreach(InputOutputData<string, NodeData> io in dialogueNodes)
        {
            if (io.Key == id)
                return io.Value;
        }
        return null;
    }
        
}
