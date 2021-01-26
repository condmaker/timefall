using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

namespace DialogueSystem.Editor
{
    /// <summary>
    /// Class responsible for managing the graphview component of
    /// the Node based Dialogue System
    /// </summary>
    public class DialogueGraphView : GraphView
    {
        /// <summary>
        /// Property that define the name of the dislayed Dialogue
        /// </summary>
        public string DialogueName { get; set; }

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public DialogueGraphView()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            AddElement(GenerateFirstNode());
        }

        /// <summary>
        /// Method responsible for getting all ports 
        /// compatible with given port.
        /// </summary>
        /// <param name="startPort">Start port to validate against</param>
        /// <param name="nodeAdapter">NodeAdapter Component</param>
        /// <returns>List of compatible ports</returns>
        public override List<Port> GetCompatiblePorts(Port startPort,
            NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach(funcCall: (port) =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }


        /// <summary>
        /// Method responsible for creating a new Port component
        /// </summary>
        /// <param name="node">Node of the new Port</param>
        /// <param name="portDirection">IO direction of the new Port</param>
        /// <param name="capacity">Amount of connections that the 
        /// new Port can have</param>
        /// <returns>New Port</returns>
        private Port GeneratePort(DialogueNode node, Direction portDirection,
            Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, 
                portDirection, capacity, typeof(string));
        }


        /// <summary>
        /// Method responsible for creating the "Start" node of the 
        /// Dialogue System
        /// </summary>
        /// <returns>The first Node</returns>
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

        /// <summary>
        /// Method responsible for creating a new empty Node
        /// </summary>
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

        /// <summary>
        /// Methog that appends components to a Node
        /// </summary>
        /// <param name="node">Node to append the components on</param>
        /// <param name="nd">NodeData with the information of what 
        /// components to append</param>
        /// <returns>The new Node with appended components</returns>
        public DialogueNode AppendDefaultItems(DialogueNode node, NodeData nd = null)
        {
            node.title = "Dialogue Node";

            Button butt = new Button(clickEvent: () =>
            {
                AddPort(node);
            });
            butt.text = "Add Choice";
            node.titleContainer.Insert(1, butt);

            //Create and Add default Input Port
            Port generateImpPort = GeneratePort(node, Direction.Input);
            generateImpPort.portName = "Prev";
            node.inputContainer.Add(generateImpPort);

            string choice = "";
            if(nd?.OutPorts.Count > 0)
                choice = nd.OutPorts[0].Name;
            //Create and Add Output Port
            AddPort(node, choice);
            

            //Create and Add a textField to input the dialogue
            TextField text = new TextField();

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
       
        /// <summary>
        /// Method responsible for addind a new Port to a Node
        /// </summary>
        /// <param name="node">Node to add the new Port on</param>
        /// <param name="choice">Name of the port corresponding 
        /// to the choice it represents</param>
        public void AddPort(DialogueNode node, string choice = "")
        {
            ChoiceComponent Cc = new ChoiceComponent(node, choice);
            Cc.AddComponent(node);
        }



        //LOAD DIALOGUE SCRIPT

        /// <summary>
        /// Method responsible for creating a new Dialogue Node
        /// based on an already existing one
        /// </summary>
        /// <param name="data">Data to base the new Node out of</param>
        public void InstatiateDialogueNode(NodeData data)
        {
            DialogueNode node = new DialogueNode
            {
                GUID = data.GUID,
                title = "",
                DialogText = data.Dialogue
            };

            node = AppendDefaultItems(node, data);
            node.SetPosition(data.Position);
        }


        /// <summary>
        /// Method responsible for adding the ports connections to the window
        /// </summary>
        /// <param name="data">Data to base the connections out of</param>
        public void InstatiateEdges(NodeData data)
        {
            //ConnectStart

            DialogueNode node = GetNode(data.GUID);

            for (int i = 0; i < data.OutPorts.Count - 1; i++)
            {
                AddPort(node, data.OutPorts[i+1].Name);
            }

            int it = 0;

            foreach (UnityEngine.UIElements.VisualElement welp in node.outputContainer.Children())
            {

                if (!(welp is Port))
                {
                    continue;
                }

                Port p = welp as Port;

                if (data.OutPorts.Count == 0) continue;

                string gui = data.OutPorts[it].ID;
                DialogueNode conNode = GetNode(gui);

                foreach (Port ort in conNode.inputContainer.Children())
                {
                    AddElement(ort.ConnectTo(p));
                }

                it++;
            }

        }

        /// <summary>
        /// Method responsible for connecting the first Node 
        /// to the "Start" Node
        /// </summary>
        /// <param name="data">Data to base the connection out of</param>
        public void ConnectToStart(NodeData data)
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

        /// <summary>
        /// Method responsible for getting a Node based on its ID
        /// </summary>
        /// <param name="id">ID of the wanted Node</param>
        /// <returns>The Node with the passed ID</returns>
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


}