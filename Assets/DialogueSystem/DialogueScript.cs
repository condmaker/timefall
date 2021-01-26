using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    /// <summary>
    /// Class responsible for storing the data of a complete dialogue script
    /// </summary>
    public class DialogueScript : ScriptableObject, IEnumerable<IOData>
    {
        /// <summary>
        /// Name of the Dialogue
        /// </summary>
        [SerializeField]
        public string DialogueName;

        /// <summary>
        /// Unique Id of the Dialogue
        /// </summary>
        private int dialogueID;

        /// <summary>
        /// List of NodeDatas and their respective keys
        /// Works in the same way as a dictionary
        /// </summary>
        [SerializeField]
        private List<IOData> dialogueNodes =
            new List<IOData>();

        /// <summary>
        /// Amount of Nodes in the Dialogue
        /// </summary>
        public int Count => dialogueNodes.Count;


        /// <summary>
        /// Method responsible for adding a new NodeData to the
        /// DialogueScript
        /// </summary>
        /// <param name="nd">NodeData to be added</param>
        public void FillDialogueDic(NodeData nd)
        {
            IOData par = new IOData(nd.GUID, nd);
            dialogueNodes.Add(par);
        }

        /// <summary>
        /// Method responsible for getting a specific NodeData based on 
        /// its index 
        /// </summary>
        /// <param name="index">Position of the NodeData on the 
        /// dialogueNodes list</param>
        /// <returns>NodeData present of the specified index</returns>
        public NodeData GetNodeByIndex(int index)
        {
            return dialogueNodes[index].data;
        }


        /// <summary>
        /// Method responsible for getting a specific NodeData based on its id
        /// This method is less efficient
        /// </summary>
        /// <param name="id">Unique id of the wanted NodeData</param>
        /// <returns>NodeData with the specified id</returns>
        public NodeData GetNodeByGUID(string id)
        {
            foreach (IOData io in dialogueNodes)
            {
                if (io.key == id)
                    return io.data;
            }
            return null;
        }


        /// <summary>
        /// Method responsible for getting the NodeData that follows 
        /// the passed one
        /// </summary>
        /// <param name="current">the current NodeData 
        /// to iterate upon</param>
        /// <param name="choice">The choice that defines which Node Data
        /// to be returned</param>
        /// <returns>The NodeData that follows the passed one</returns>
        public NodeData GetNextNode(NodeData current, int choice = 0)
        {
            if (current.OutPorts.Count > 0)
                return GetNodeByGUID(current.OutPorts?[choice].ID);
            return null;
        }


        /// <summary>
        /// Method responsible for iterating the list of IOData
        /// and returning them one by one
        /// </summary>
        /// <returns>The next IOData in line</returns>
        public IEnumerator<IOData> GetEnumerator()
        {
            foreach (IOData io in dialogueNodes)
            {
                yield return io;
            }
        }

        /// <summary>
        /// Method responsible for allowing the use of foreach in 
        /// with this class
        /// </summary>
        /// <returns>The next IOData in line</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
    
    }
}


