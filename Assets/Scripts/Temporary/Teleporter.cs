using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleports the player around the scene.
/// </summary>
public class Teleporter : MonoBehaviour
{
    /// <summary>
    /// Object that will be teleported.
    /// </summary>
    [SerializeField]
    private GameObject ObjectToTeleport;
    //private LinkedList<GameObject> TeleportPoints;

    /// <summary>
    /// Teleport points that can be cycled through.
    /// </summary>
    [SerializeField]
    private GameObject[] TeleportPoints;

    /// <summary>
    /// The index of the current Teleport Point.
    /// </summary>
    private int currentTpPoint;

    private void Start() => currentTpPoint = -1;

    private void Update()
    {
        if (PlayerPrefs.GetInt("teleport") == 0) return;

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            PreviousPoint();
            ObjectToTeleport.transform.position = 
                TeleportPoints[currentTpPoint].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            NextPoint();
            ObjectToTeleport.transform.position = 
                TeleportPoints[currentTpPoint].transform.position;
        }

        //ObjectToTeleport.transform.position 
        // = TeleportPoints[currentTpPoint].transform.position;
    }

    /// <summary>
    /// Cycles the array to the next Teleport Point
    /// </summary>
    private void NextPoint()
    {
        currentTpPoint++;
        if (currentTpPoint == TeleportPoints.Length)
            currentTpPoint = 0;
    }

    /// <summary>
    /// Cycles the array to the previous Teleport Point
    /// </summary>
    private void PreviousPoint()
    {
        currentTpPoint--;
        if (currentTpPoint < 0)
            currentTpPoint = TeleportPoints.Length - 1;
    }
}
