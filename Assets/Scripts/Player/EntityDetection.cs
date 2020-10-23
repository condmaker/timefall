using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

// !!! Sync EntityDetection with PlayerInput after timers have been sorted

public class EntityDetection : MonoBehaviour
{
    private PlayerInput pI;

    // Variable that stores the GameObject collided with
    [SerializeField]
    private GameObject objectTouched;
    public string objectTouchedName;

    // Bool that specifies is the player is colliding with an object
    private bool isColliding;
    public bool ObjectIsNPC { get; set; }
    public bool ObjectIsUsable { get; set; }
    public bool ObjectIsGrabable { get; set; }

    void Start()
    {
        pI = GetComponentInParent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if (objectTouched != null)
        {
            CheckTag(objectTouched);
            objectTouchedName = objectTouched.name;
            if (!pI.IsWalking && Input.GetKey("e") && isColliding)
            {
                if (ObjectIsGrabable)
                { 
                  //put in inventory
                  Destroy(objectTouched);
                  ResetBools();
                }
                if (ObjectIsNPC) { }
                //talk
                if (ObjectIsUsable) { }
                //use (door)
            }
        }
    }
    // This method is called when the EntityDetection object collides with other triggers
    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        objectTouched = other.gameObject;
    }
    // This method is called when the EntityDetetction object stops colliding with other triggers
    private void OnTriggerExit(Collider other)
    {
        ResetBools();
        objectTouched = null;
    }
    private void CheckTag(GameObject _objectTouched)
    {
            if (objectTouched.tag == "Grabable")
                ObjectIsGrabable = true;

            if (objectTouched.tag == "Usable")
                ObjectIsUsable = true;

            if (objectTouched.tag == "NPC")
                ObjectIsNPC = true;
    }
    private void ResetBools()
    {
        isColliding = false;
        ObjectIsGrabable = false;
        ObjectIsUsable = false;
        ObjectIsNPC = false;
    }
}
