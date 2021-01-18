using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueSystem
{
    [Serializable]
    public struct IOData
    {
        public IOData(string k, NodeData d)
        {
            key = k;
            data = d;
        }

        public string key;
        public NodeData data;
    }
}
