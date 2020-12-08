using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{

    public DialogueScript aa;

    // Start is called before the first frame update
    void Start()
    {

        print(aa.Count);
        print("Start");
        //print(aa.GetNodeByIndex(0).OutPorts[0]);


        //print(aa.GetNodeByIndex(2).Dialogue);

        print(aa.GetNodeByGUID(aa.GetNodeByIndex(0).OutPorts[0]));
        print("End");
    }

}
