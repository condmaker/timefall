using System;
using System.Collections.Generic;

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


    public NodeData(string guID, string dialogue, List<string> outPorts)
    {
        GUID = guID;
        Dialogue = dialogue;
        OutPorts = outPorts;
    }
}
















