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

    public List<InputOutputData<string, NodeData>> dialogueNodes =
        new List<InputOutputData<string, NodeData>>();
   

    public void FillDialogueDic(NodeData nd)
    {
        
        InputOutputData<string, NodeData> par = new InputOutputData<string, NodeData>(nd.GUID, nd);
        Debug.Log(par.Key);
        dialogueNodes.Add(par);
    }


}
