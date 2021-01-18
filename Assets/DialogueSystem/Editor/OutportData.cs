using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DialogueSystem.Editor
{
    public struct OutportData
    {
        public string Name { get; }
        public string ID { get; }

        public OutportData(string name, string id)
        {
            Name = name;
            ID = id;
        }

    }
}
