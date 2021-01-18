using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class NodeData
    {

        [SerializeField] [HideInInspector]
        private string guid;
        [SerializeField]
        private string dialogue;
        [SerializeField] [HideInInspector]
        private Rect position;
        [SerializeField] [HideInInspector]
        private bool isStart;


        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }
        public string Dialogue
        {
            get { return dialogue; }
            set { dialogue = value; }
        }
        public Rect Position => position;
        public bool IsStart => isStart;

        [HideInInspector]
        public List<OutportData> OutPorts = new List<OutportData>();


        public NodeData(string guID, string dialogue, Rect pos, bool start, List<OutportData> outPorts)
        {
            isStart = start;
            position = pos;
            GUID = guID;
            Dialogue = dialogue;
            OutPorts = outPorts;
        }
    }
}















