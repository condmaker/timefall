using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

// !!! Sync EntityDetection with PlayerInput after timers have been sorted

public class EntityDetection : MonoBehaviour
{
    private PlayerInput pI;

    // Variable that stores the GameObject collided with
    [SerializeField]
    private GameObject objectTouched;
    private GameObject textDisplayObject;

    private TextMeshProUGUI textToDisplay;

    // Bool that specifies is the player is colliding with an object
    private bool isColliding;

    void Start()
    {
        pI                = GetComponentInParent<PlayerInput>();
        textDisplayObject = GameObject.Find("InteractionDisplay"); //!!! CHANGE THIS LATER !!!
        textToDisplay     = textDisplayObject.GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (!pI.IsWalking && Input.GetKey("e") && isColliding)
        {
            //if object has tag... (NPC, door/gate, object?)
            textToDisplay.text = "You picked up a " + objectTouched.name;
            //get object ID call inventory function to store object ?
            Destroy(objectTouched);
        }
    }

    // This method is called when the EntityDetection object collides with other triggers
    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        objectTouched = other.gameObject;
        textToDisplay.text = "Press 'E' to pick-up";
    }
    // This method is called when the EntityDetetction object stops colliding with other triggers
    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        objectTouched = null;
        textToDisplay.text = " ";
    }
    private void CheckTag(GameObject objectTouched)
    {
        //if (objectTouched.tag == "gate") 
    }
}
