using System;
using System.Collections.Generic;

[System.Serializable]
public class NodeData
{
    public string GUID { get; }
    public string Dialogue { get; }
    public List<string> OutPorts = new List<string>();


    public NodeData(string guID, string dialogue, List<string> outPorts)
    {
        GUID = guID;
        Dialogue = dialogue;
        //OutPorts = new List<string>() {"i"};
        OutPorts = outPorts;
    }
}
















