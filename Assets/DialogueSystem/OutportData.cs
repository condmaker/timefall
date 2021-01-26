using System;

namespace DialogueSystem
{

    /// <summary>
    /// Struct responsible for storing the choice text and
    /// its respective id
    /// </summary>
    [Serializable]
    public struct OutportData
    {

        /// <summary>
        /// Text of a Choice Component
        /// </summary>
        [UnityEngine.SerializeField]
        private  string name;
        
        /// <summary>
        /// Unique Id that represents the specific Choice
        /// </summary>
        [UnityEngine.SerializeField]
        private  string id;

        /// <summary>
        /// Property that defines the choice text
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Property that defines the unique id of the specific Choice
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Constructor of this struct
        /// </summary>
        /// <param name="name">Choice text</param>
        /// <param name="id">Unique id of the Choice</param>
        public OutportData(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

    }
}
