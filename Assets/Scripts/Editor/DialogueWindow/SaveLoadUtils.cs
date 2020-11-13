using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class SaveLoadUtils 
{
 
    public static void SaveDialogues(string dialogueName, string file, GraphView view)
    {
        //string path = $"{file}/{dialogueName}.asset";

        string path = $"Assets/temp.asset";
        DialogueScript temp = ScriptableObject.CreateInstance<DialogueScript>();
      
        foreach (Node n in view.nodes.ToList())
        {
            DialogueNode nd = n as DialogueNode;
            NodeData data = new NodeData(
                guID: nd.GUID,
                dialogue: nd.DialogText,
                outPorts: nd.OutPorts
                );

            temp.FillDialogueDic(data);
        }    
        temp.DialogueName = dialogueName;


        AssetDatabase.CreateAsset(temp, path);
        AssetDatabase.SaveAssets();

    }

    

}
