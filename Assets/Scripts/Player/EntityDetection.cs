// Sync EntityDetection with PlayerInput after timers have been sorted

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityDetection : MonoBehaviour
{
    private PlayerInput pI;

    // Variable that stores the GameObject collided with
    [SerializeField]
    private GameObject objectTouched;

    // Bool that specifies is the player is colliding with an object
    private bool isColliding;

    void Start()
    {
        pI = GetComponentInParent<PlayerInput>();
    }

    void FixedUpdate()
    {
        //Debug.Log(pI.IsInteracting);
        //Debug.Log(pI.IsWalking);
        if (!pI.IsWalking && Input.GetKey("e") && isColliding )
            //get object ID call inventory function to store object ?
            Destroy(objectTouched);
    }

    // This method is called when the EntityDetetction object collides with other triggers
    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        objectTouched = other.gameObject;
    }
    // This method is called when the EntityDetetction object stops colliding with other triggers
    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        objectTouched = null;
    }
}
