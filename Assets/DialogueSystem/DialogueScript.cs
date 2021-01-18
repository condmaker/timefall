using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueScript : ScriptableObject, IEnumerable<IOData>
    {


        [SerializeField]
        public string DialogueName;
        private int dialogueID;

        [SerializeField]
        private List<IOData> dialogueNodes =
            new List<IOData>();

        public int Count => dialogueNodes.Count;


        public void FillDialogueDic(NodeData nd)
        {
            IOData par = new IOData(nd.GUID, nd);
            dialogueNodes.Add(par);
        }

        public NodeData GetNodeByIndex(int index)
        {
            return dialogueNodes[index].data;
        }

        //Gets the node using its id
        //This option is less efficient 
        public NodeData GetNodeByGUID(string id)
        {
            foreach (IOData io in dialogueNodes)
            {
                if (io.key == id)
                    return io.data;
            }
            return null;
        }

        public NodeData GetNextNode(NodeData current, int choice = 0)
        {
            if (current.OutPorts.Count > 0)
                return GetNodeByGUID(current.OutPorts?[choice].ID);
            return null;
        }



        public IEnumerator<IOData> GetEnumerator()
        {
            foreach (IOData io in dialogueNodes)
            {
                yield return io;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


