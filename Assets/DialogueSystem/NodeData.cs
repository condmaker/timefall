using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeData
{
    [UnityEngine.SerializeField]
    private string guid;

    [UnityEngine.SerializeField]
    private string dialogue;

    public string GUID 
    { 
        get { return guid; }
        set { guid = value; }
    }
    
    public string Dialogue 
    { 
        get { return dialogue; } 
        set { dialogue = value; } 
    }

    public List<string> OutPorts = new List<string>();

    public Rect Position { get; set; }
    

    public NodeData(string guID, string dialogue, Rect pos, List<string> outPorts)
    {
        Position = pos;
        GUID = guID;
        Dialogue = dialogue;
        OutPorts = outPorts;
    }
}
















