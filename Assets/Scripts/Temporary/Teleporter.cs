using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToTeleport;
    [SerializeField]
    //private LinkedList<GameObject> TeleportPoints;
    private GameObject[] TeleportPoints;

    private int currentTpPoint;

    void Start()
    {
        currentTpPoint = -1;
    }

    // Update is called once per frame
    void Update()
    {
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

        //ObjectToTeleport.transform.position = TeleportPoints[currentTpPoint].transform.position;
    }

    private void NextPoint()
    {
        currentTpPoint++;
        if (currentTpPoint == TeleportPoints.Length)
            currentTpPoint = 0;
    }

    private void PreviousPoint()
    {
        currentTpPoint--;
        if (currentTpPoint < 0)
            currentTpPoint = TeleportPoints.Length - 1;
    }
}
