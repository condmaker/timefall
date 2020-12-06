using System;

[System.Serializable]
public class NodeData
{
    public string GUID { get; }
    public string Dialogue { get; }
    public int[] OutPorts { get; }


    public NodeData(string guID, string dialogue, int[] outPorts)
    {
        GUID = guID;
        Dialogue = dialogue;
        OutPorts = outPorts;
    }
}
















