using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//https://answers.unity.com/questions/634110/associate-my-custom-asset-with-a-custom-editorwind.html
public class TesteHandler : MonoBehaviour
{
    [UnityEditor.Callbacks.OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject as DialogueScript != null)
        {
            DialogueGraph.OpenDialogueGraphWindow(Selection.activeObject as DialogueScript);
            return true; //catch open file
        }

        return false; // let unity open the file
    }
}
