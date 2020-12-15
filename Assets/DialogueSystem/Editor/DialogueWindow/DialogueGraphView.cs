using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEditor;

public class DialogueGraphView : GraphView
{

    public DialogueGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GenerateFirstNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        ports.ForEach(funcCall:(port) => {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    private Port GeneratePort(DialogueNode node, Direction portDirection, 
        Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(string));
    }


    private DialogueNode GenerateFirstNode()
    {
        DialogueNode node = new DialogueNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            DialogText = "ENTRYPOINT",
            EntryPoint = true
        };

        Port generatePort = GeneratePort(node, Direction.Output);
        generatePort.portName = "Next";
        node.outputContainer.Add(generatePort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }



    
    public void CreateDialogueNode()
    {
        
        DialogueNode node = new DialogueNode
        {            
            GUID = Guid.NewGuid().ToString(),
            title = "",
            DialogText = ""
        };
        AppendDefaultItems(node);
    }
    public DialogueNode AppendDefaultItems(DialogueNode node, NodeData nd = null)
    {
        node.title = "Dialogue Node";

        Button butt = new Button(clickEvent: () =>
        {
            AddPort(node);
        });
        butt.text = "Add Choice";
        node.titleContainer.Insert(1, butt);


        //Create and Add default Output Port
        Port generateImpPort = GeneratePort(node, Direction.Input);
        generateImpPort.portName = "Prev";
        node.inputContainer.Add(generateImpPort);

        //Create and Add Inpunt Port
        Port generateOutPort = GeneratePort(node, Direction.Output);
        generateOutPort.portName = "Next";
        node.outputContainer.Add(generateOutPort);

        //Create and Add a textField to input the dialogue
        TextField text = new TextField();

        //Testing Only THIS EVENT NEEDS TO BE CHANGED
        //Instead of onChange it needs to be "when the user ENDS the change"
        text.RegisterCallback<ChangeEvent<string>>((ChangeEvent<string> evt) =>
        {
            node.DialogText = evt.newValue;
        });
        text.multiline = true;
        text.value = $"{nd?.Dialogue}\n\n\n";
        node.mainContainer.Insert(1, text);

        node.RefreshExpandedState();
        node.RefreshPorts();

        AddElement(node);

        return node;
    }
    private void AddPort(DialogueNode node)
    {
        Port generateOutPort = GeneratePort(node, Direction.Output);
        generateOutPort.portName = "Next";
        node.outputContainer.Add(generateOutPort);
    }



    //LOAD DIALOGUE SCRIPT

    internal void InstatiateDialogueNode(NodeData nd)
    {
        DialogueNode node = new DialogueNode
        {
            GUID = nd.GUID,
            title = "",
            DialogText = nd.Dialogue
        };

        node = AppendDefaultItems(node, nd);
        node.SetPosition(nd.Position);
    }

    internal void InstatiateEdges(NodeData nd)
    {
        //ConnectStart

        DialogueNode node = GetNode(nd.GUID);

        for(int i = 0; i < nd.OutPorts.Count-1; i++)
        {
            AddPort(node);
        }

        int it = 0;
      
        foreach (Port p in node.outputContainer.Children())
        {
            if (nd.OutPorts.Count == 0) continue;
            
            string gui = nd.OutPorts[it];
            DialogueNode conNode = GetNode(gui);

            foreach(Port ort in conNode.inputContainer.Children())
            {
                AddElement(ort.ConnectTo(p));
            }       
            
            it++;
        }

    }

    internal void ConnectToStart(NodeData data)
    {
        DialogueNode start = nodes.First() as DialogueNode;


        foreach (Port p in start.outputContainer.Children())
        {                
            foreach (Port ort in GetNode(data.GUID).inputContainer.Children())
            {
                AddElement(ort.ConnectTo(p));
            }
        }
    }


    //KILL ME PLZ
    public DialogueNode GetNode(string id)
    {
        DialogueNode z = null;

        nodes.ForEach(x => 
        {
            DialogueNode y = (x as DialogueNode);
            if (y.GUID == id)
                z = y;       
        });

        return z;

    }

   

  
}
