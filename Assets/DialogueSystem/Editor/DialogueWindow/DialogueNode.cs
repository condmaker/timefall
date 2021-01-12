using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class DialogueNode: Node
{
    public string GUID { get; set; }
    public string DialogText { get; set; }
    public bool EntryPoint { get; set; }
    public int[] OutPorts { get; set; }

}
