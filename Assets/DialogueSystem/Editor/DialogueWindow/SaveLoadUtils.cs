using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class SaveLoadUtils 
{
 
    public static void SaveDialogues(string dialogueName, string file, GraphView view)
    {
        string path = $"{file}/{dialogueName}.asset";
        //string path = $"Assets/temp.asset";
        DialogueScript temp = ScriptableObject.CreateInstance<DialogueScript>();
      
        foreach (Node n in view.nodes.ToList())
        {
            

            DialogueNode nd = n as DialogueNode;

            bool isConnected = false;

            //Check if they have any imput connections
            foreach( Port p in n.inputContainer.Children())
            {
                if (p.connected)
                {
                    isConnected = true;
                }
            }

            if (!isConnected)
                continue;


            List<string> outPort = new List<string>();
            //Create list of Outport IDs
            foreach (Port p in n.outputContainer.Children())
            {
                if (p.connected)
                {
                    foreach (Edge e in p.connections)
                    {
                        DialogueNode outNode = e.input.node as DialogueNode;
                        outPort.Add(outNode.GUID);
                    }
                }
            }



            NodeData data = new NodeData(
                guID: nd.GUID,
                dialogue: nd.DialogText,
                outPorts: outPort
                );

            temp.FillDialogueDic(data);
        }    
        temp.DialogueName = dialogueName;


        AssetDatabase.CreateAsset(temp, path);
        AssetDatabase.SaveAssets();

    }

    

}
