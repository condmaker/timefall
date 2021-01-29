using UnityEditor;
using UnityEngine;
using DialogueSystem;
using DialogueSystem.Editor;

/// <summary>
/// Class responsible for making Dialogue objects double clickable.
/// </summary>
/// <remarks>
/// Based on code found <a href="https://answers.unity.com/questions/634110/associate-my-custom-asset-with-a-custom-editorwind.html">here</a>
/// </remarks>
public class TesteHandler : MonoBehaviour
{
    
    /// <summary>
    /// Makes Dialogue objects openable.
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    [UnityEditor.Callbacks.OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        string assetPath = AssetDatabase.GetAssetPath(instanceID);
        DialogueScript scriptableObject = AssetDatabase.LoadAssetAtPath<DialogueScript>(assetPath);
        if (scriptableObject != null)
        {
            DialogueGraph.OpenDialogueGraphWindow(scriptableObject);
            return true;
        }
        return false; //let unity open it.
    }
}