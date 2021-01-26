using UnityEditor.Experimental.GraphView;

/// <summary>
/// Class responsible for storing the data of a DialogueNode
/// </summary>
public class DialogueNode: Node
{
    /// <summary>
    /// Property that defines the unique GUID of the Node
    /// </summary>
    public string GUID { get; set; }

    /// <summary>
    /// Text component of the Dialogue Node
    /// </summary>
    public string DialogText { get; set; }

    /// <summary>
    /// Property that defines if this node is connected to the Node "Start"
    /// </summary>
    public bool EntryPoint { get; set; }

    /// <summary>
    /// Property that defines the list of output ports connected to other Nodes
    /// </summary>
    public int[] OutPorts { get; set; }

}
