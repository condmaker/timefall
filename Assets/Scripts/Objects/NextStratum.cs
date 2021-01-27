using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Class responsible for loading the next scene when the state of the
/// GameObject changes
/// </summary>
public class NextStratum : MonoBehaviour
{

    /// <summary>
    /// Name of the next scene the class will load
    /// </summary>
    [SerializeField]
    private string sceneName;

    /// <summary>
    /// Object State Handler of Object this script is attached
    /// </summary>
    private ObjectStateHandler osh;


    /// <summary>
    /// This method is called when the scene starts
    /// </summary>
    public void Awake()
    {
        osh = GetComponent<ObjectStateHandler>();
        if (osh != null)
            osh.OnChangeState += Next;
    }


    /// <summary>
    /// Method responsible for loading the scene specified
    /// </summary>
    /// <param name="oSH">Object state Handler attached to this object</param>
    /// <param name="state">State of the OSH attached</param>
    public void Next(ObjectStateHandler oSH, short state)
    {
        try
        {
            StratumManager.instance.SceneString = sceneName;
            // Change to index later
            SceneManager.LoadScene("Loading");
        }
        catch (Exception)
        {
            print("Scene name invalid!");
        }
    }

}
