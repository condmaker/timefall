﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditorInternal;
using UnityEngine;

// !!! Sync EntityDetection with PlayerInput after timers have been sorted

// Everything related to the player interacting in any way with the environment
// is treated here (like Raycasts for example)

public class EntityDetection : MonoBehaviour
{
    [SerializeField]
    private PlayerInput pI;

    [SerializeField]
    private InventoryHandler inventory;

    // Variable that stores the GameObject collided with
    public GameObject ObjectTouched { get; private set; }

    [SerializeField]
    private MessageDisplay mD;
    private ObjectData objectData;
    private Toggler toggler;

    // Bool that specifies is the player is colliding with an object
    public bool IsColliding { get; private set; }
    private RaycastHit currentWorldObject;


    private void Update()
    {
        if (IsColliding)
        {
            // Definetly change this to do it one time.
            ObjectTouched = currentWorldObject.transform.gameObject;
            // This could be better
            if ((ObjectTouched.layer == 8) || (ObjectTouched.layer == 9))
            {
                objectData = ObjectTouched.GetComponent<DataHolder>().GetData;
                // This only appears for certain objects like levers and stuff,
                // but not for other world objects like walls, so we need to 
                // code that in
                mD.DisplayMessage(objectData);
            }
        }
        else
        {
            ObjectTouched = null;
            mD.CleanMessage();
        }

        // Some of this can be placed in OTriggerEnter
        if (ObjectTouched != null)
        {
            if (!pI.IsWalking && Input.GetKeyDown("e") && IsColliding)
            {
                switch (objectData.InteractionType)
                {
                    case InteractionType.isGrabable:
                        inventory.AddItem(objectData as ItemData);
                        Destroy(ObjectTouched);
                        mD.CleanMessage();
                        break;
                    case InteractionType.isUsable:
                        //objectTouched.toggle? (switches bool )
                        toggler = ObjectTouched.GetComponent<Toggler>();
                        toggler.Toggle();
                        break;
                    case InteractionType.isNPC:
                        // talk
                        break;
                    default:
                        print("Porque é que essa coisa é trigger ?");
                        break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        IsColliding = Physics.Raycast(
            transform.position, transform.forward, out currentWorldObject, 
            pI.MoveDistance);
    }
}