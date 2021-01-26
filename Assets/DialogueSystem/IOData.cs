using System;

namespace DialogueSystem
{
    /// <summary>
    /// Struct responsible for storing the data of a 
    /// Dialogue and its respective key value
    /// </summary>
    [Serializable]
    public struct IOData
    {
        /// <summary>
        /// Constructor of this struct
        /// </summary>
        /// <param name="k">Unique id of the NodeData</param>
        /// <param name="d">Data of the respective Node</param>
        public IOData(string k, NodeData d)
        {
            key = k;
            data = d;
        }

        /// <summary>
        /// Unique key used to access the connected NodeData
        /// </summary>
        public string key;

        /// <summary>
        /// Data of the Node
        /// </summary>
        public NodeData data;
    }
}
