using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToTeleport, teleportPoint1, teleportPoint2, teleportPoint3, teleportPoint4, teleportPoint5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            ObjectToTeleport.transform.position = teleportPoint1.transform.position;
        if (Input.GetKeyDown(KeyCode.U))
            ObjectToTeleport.transform.position = teleportPoint2.transform.position;
        if (Input.GetKeyDown(KeyCode.I))
            ObjectToTeleport.transform.position = teleportPoint3.transform.position;
        if (Input.GetKeyDown(KeyCode.O))
            ObjectToTeleport.transform.position = teleportPoint4.transform.position;
        if (Input.GetKeyDown(KeyCode.P))
            ObjectToTeleport.transform.position = teleportPoint5.transform.position;
    }
}
