﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class SaveLoadUtils 
{
    //This all needs a rework  
    public static void SaveDialogues(GraphView view, string dialogueName)
    {
        if (dialogueName == null)
            dialogueName = "InitialName";

        //https://answers.unity.com/questions/437391/prompting-dialogue-box-for-input-in-editor.html
        string path = 
            EditorUtility.SaveFilePanelInProject("Save Your Dialogue",
            dialogueName + ".asset", "asset", 
            "Please select file name to save dialogue to:",
            "Assets/DialogueSystem/Dialogues");

        if (string.IsNullOrEmpty(path)) return;

        DialogueScript temp = ScriptableObject.CreateInstance<DialogueScript>();
      
        
        foreach (Node n in view.nodes.ToList())
        {           
            DialogueNode nd = n as DialogueNode;

            bool isConnected = false;
            bool isStart = false;

            //Check if they have any imput connections
            foreach( Port p in n.inputContainer.Children())
            {
                if (p.connected)
                {
                    isConnected = true;

                    //Check if the connection is the start
                    foreach (Edge e in p.connections)
                    {
                        DialogueNode outNode = e.output.node as DialogueNode;
                        if (outNode.title == "Start") isStart = true;
                    }
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
                start: isStart,
                pos: nd.GetPosition(),
                guID: nd.GUID,
                dialogue: nd.DialogText,
                outPorts: outPort
                );

            temp.FillDialogueDic(data);
        }



        string[] dir = path.Split('/');

        temp.DialogueName = dir[dir.Length - 1].Replace(".asset","");


        AssetDatabase.CreateAsset(temp, path);
        AssetDatabase.SaveAssets();
    }

    public static void LoadDialogues(DialogueGraphView view, DialogueScript script)
    {
        view.DialogueName = script.DialogueName;

        foreach (IOData io in script)
        {
            view.InstatiateDialogueNode(io.data);
        }

        
        foreach (IOData io in script)
        {
            if (io.data.IsStart) view.ConnectToStart(io.data);
            view.InstatiateEdges(io.data);
        }

    }

}