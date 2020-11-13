using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{

    public DialogueScript aa;

    // Start is called before the first frame update
    void Start()
    {

        print(aa.dialogueNodes.Count);
        print("Start");
        for (int i = 0; i < aa.dialogueNodes.Count; i++)
        {
            print(aa.dialogueNodes[i].Key);
        }
        print("End");
    }

}
