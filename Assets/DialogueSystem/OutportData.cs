using System;

namespace DialogueSystem
{
    [Serializable]
    public struct OutportData
    {
        [UnityEngine.SerializeField]
        private  string name;
        [UnityEngine.SerializeField]
        private  string id;

        public string Name => name;
        public string ID => id;

        public OutportData(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

    }
}
