using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    private DataHolder objectHolder;
    private IManualInteractor interactor;

    // Bool that specifies is the player is colliding with an object
    public bool IsColliding { get; private set; }
    private RaycastHit currentWorldObject;


    private void Update()
    {
        IsColliding = Physics.Raycast(
            transform.position, transform.forward, out currentWorldObject,
            pI.MoveDistance);

        if (!pI.CanInput) return;

        if (IsColliding)
        {
            // Definetly change this to do it one time.
            ObjectTouched = currentWorldObject.transform.gameObject;
            // This could be better
            if ((ObjectTouched.layer == 8) || (ObjectTouched.layer == 9))
            {
                objectHolder = ObjectTouched.GetComponent<DataHolder>();
                objectData = objectHolder.GetData(inventory.equipedItem);
                
                // This only appears for certain objects like levers and stuff,
                // but not for other world objects like walls, so we need to 
                // code that in
                mD.DisplayMessage(objectData);
            }
        }
        else
        {
            IsColliding = false;
            objectData = null;
            ObjectTouched = null;
            mD.CleanMessage();
        }

        if (ObjectTouched != null)
        {
            if (!pI.IsWalking && Input.GetKeyDown("e") && IsColliding)
            {
                if (objectData == null) return;

                switch (objectData.InteractionType)
                {
                    case InteractionType.isGrabable:
                        inventory.AddItem(objectData as ItemData);
                        objectHolder.DestroyObject();
                        //Destroy(ObjectTouched);
                        mD.CleanMessage();
                        break;
                    case InteractionType.isUsable:
                        //objectTouched.toggle? (switches bool)
                        interactor = ObjectTouched.GetComponent<IManualInteractor>();
                        bool itemused  = interactor.Toggle(inventory?.equipedItem);
                        if (itemused)
                        {
                            inventory.ClearEquiped();
                        }
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

}
