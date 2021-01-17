using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

// !!! Sync EntityDetection with PlayerInput after timers have been sorted

// Everything related to the player interacting in any way with the environment
// is treated here (like Raycasts for example)

public class EntityDetection : MonoBehaviour
{
    private PlayerInput pI;
    private PlayerMovement pM;

    [SerializeField]
    private InventoryHandler inventory;

    [SerializeField]
    private DialogueDisplayHandler dialogueHandler;

    // Variable that stores the GameObject collided with
    public GameObject ObjectTouched { get; private set; }

    [SerializeField]
    private MessageDisplay mD;
    private ObjectData objectData;
    private DataHolder objectHolder;
    private ManualInteractor interactor;

    // Bool that specifies is the player is colliding with an object
    public bool IsColliding { get; private set; }
    private RaycastHit currentWorldObject;

    public void Awake()
    {
        pI = GetComponent<PlayerInput>();
        pM = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (pI.IsStrafingRight)
        {
            Vector3 uV = new Vector3(0.0f, -1.0f, 0.0f);

            IsColliding = Physics.Raycast(
                transform.position, Quaternion.AngleAxis(
                90, transform.forward) * uV, out currentWorldObject,
                pI.MoveDistance);
        }
        else if (pI.IsStrafingLeft)
        {
            Vector3 uV = new Vector3(0.0f, -1.0f, 0.0f);

            IsColliding = Physics.Raycast(
                transform.position, Quaternion.AngleAxis(
                -90, transform.forward) * uV, out currentWorldObject,
                pI.MoveDistance);
        }
        else
        {
            IsColliding = Physics.Raycast(
                transform.position, transform.forward, out currentWorldObject,
                pI.MoveDistance);
        }


        if (!pI.CanInput)
        {
            if (pI.Bump)
            {
                GameObject temp = currentWorldObject.transform.gameObject;
                temp.GetComponent<BreakingWall>()?.Break();
            }

            return;
        }

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
            else if (ObjectTouched.layer == 13)
            {
                ObjectTouched.GetComponent<NextStratum>().Next();
            }
        }
        else
        {
            IsColliding = false;
            objectData = null;
            ObjectTouched = null;
            mD.CleanMessage();
        }

        if ((ObjectTouched != null) 
            && !pI.IsStrafingLeft && !pI.IsStrafingRight)
        {
            if (!pI.IsWalking && pI.IsInteracting && IsColliding)
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
                        interactor = ObjectTouched.GetComponent<ManualInteractor>();
                        bool itemused  = 
                            interactor.Toggle(inventory?.equipedItem, transform.position);
                        if (itemused)
                        {
                            inventory.ClearEquiped();
                        }
                        break;
                    case InteractionType.isNPC:
                        dialogueHandler.StartDialolgue((objectData as NpcData).Dialogue);
                        mD.CleanMessage();
                        this.enabled = false;
                        pM.enabled = false;
                        dialogueHandler.endDialogue += EndDialogue;
                        break;
                    default:
                        print("Porque é que essa coisa é trigger ?");
                        break;
                }
            }

            pI.IsInteracting = false;
        }



    }

    private void EndDialogue()
    {
        this.enabled = true;
        pM.enabled = true;
        dialogueHandler.endDialogue -= EndDialogue;
    }
}
