using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditorInternal;
using UnityEngine;

// !!! Sync EntityDetection with PlayerInput after timers have been sorted

public class EntityDetection : MonoBehaviour
{
    [SerializeField]
    private PlayerInput pI;

    [SerializeField]
    private InventoryHandler inventory;

    // Variable that stores the GameObject collided with
    [SerializeField]
    private GameObject objectTouched;
    [SerializeField]
    private MessageDisplay mD;
    private ObjectData objectData;
    private Toggler toggler;

    // Bool that specifies is the player is colliding with an object
    private bool isColliding;


    private void Update()
    {
        //Some pf this can be placed in OTriggerEnter
        if (objectTouched != null)
        {
            if (!pI.IsWalking && Input.GetKeyDown("e") && isColliding)
            {
                switch (objectData.InteractionType)
                {
                    case InteractionType.isGrabable:
                        inventory.AddItem(objectData as ItemData);
                        Destroy(objectTouched);
                        mD.CleanMessage();
                        break;
                    case InteractionType.isUsable:
                        //objectTouched.toggle? (switches bool )
                        toggler = objectTouched.GetComponent<Toggler>();
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
    // This method is called when the EntityDetection object collides with other triggers
    private void OnTriggerEnter(Collider other)
    {
        objectTouched = other.gameObject;
        //maybe muda isto se arranjarmos algo melhor
        if (objectTouched.GetComponent<DataHolder>().GetData() != null)
            objectData = objectTouched.GetComponent<DataHolder>().GetData();
        mD.DisplayMessage(objectData);
        isColliding = true;
    }
    // This method is called when the EntityDetetction object stops colliding with other triggers
    private void OnTriggerExit(Collider other)
    {
        objectTouched = null;
        mD.CleanMessage();
    }
}
