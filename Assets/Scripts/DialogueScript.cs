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

    [SerializeField]
    private List<IOData> dialogueNodes =
        new List<IOData>();

    public int Count => dialogueNodes.Count; 

    public void FillDialogueDic(NodeData nd)
    {
        IOData par = new IOData(nd.GUID, nd);
        dialogueNodes.Add(par);
    }

    public NodeData GetNodeByIndex(int index)
    {
        return dialogueNodes[index].data;
    }

    //Gets the node using its id
    //This option is less efficient 
    public NodeData GetNodeByGUID(string id)
    {
        foreach(IOData io in dialogueNodes)
        {
            if (io.GUID == id)
                return io.data;
        }
        return null;
    }
        
}


[Serializable]
public class IOData
{

    public IOData(string g, NodeData d)
    {
        GUID = g;
        data = d;
    }

    public string GUID;
    public NodeData data;
}
