using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using DialogueSystem;

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

    [SerializeField]
    private DialogueScript wrongInteaction;

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
        Vector3 uV = new Vector3(0.0f, -1.0f, 0.0f);

        Ray ray = new Ray(transform.position, transform.forward);

        if (pI.IsStrafingRight)
        {
            ray = new Ray(transform.position, Quaternion.AngleAxis(
                90, transform.forward) * uV);
        }
        else if (pI.IsStrafingLeft)
        {
            ray = new Ray(transform.position, Quaternion.AngleAxis(
                -90, transform.forward) * uV);
        }
        else if (pI.IsWalkingBack)
        {
            ray = new Ray(transform.position, -transform.forward);
        }

        IsColliding = Physics.Raycast(ray, out currentWorldObject,
                pI.MoveDistance, ~0, QueryTriggerInteraction.Ignore);



        if (!pI.CanInput)
        {
            if (pI.Bump)
            {

                GameObject temp = currentWorldObject.transform?.gameObject;
                temp?.GetComponent<BreakingWall>()?.Break();
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
                objectData = objectHolder?.GetData(inventory.equipedItem);

                mD.DisplayMessage(objectData);
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
                            mD.CleanMessage();
                            objectData = null;
                            break;
                        case InteractionType.isUsable:                           
                            interactor =
                                ObjectTouched.GetComponent<ManualInteractor>();

                            InteractionResult itemused =
                                interactor.Toggle(
                                    inventory?.equipedItem, transform.position);

                            switch (itemused)
                            {
                                case InteractionResult.WrongIntMessage:
                                    StartDialogue(wrongInteaction);
                                    break;
                                case InteractionResult.UseItem:
                                    inventory.ClearEquiped();
                                    break;
                            }
                            break;
                        case InteractionType.isExit:
                            interactor =
                                ObjectTouched.GetComponent<ManualInteractor>();
                                interactor.Toggle(
                                    inventory?.equipedItem, transform.position);
                            break;
                        case InteractionType.isNPC:
                            StartDialogue((objectData as NpcData).Dialogue);
                            break;
                        default:
                            print("Porque é que essa coisa é trigger ?");
                            break;
                    }
                }

                pI.IsInteracting = false;
            }
        }
    }

    private void StartDialogue(DialogueScript dS)
    {
        dialogueHandler.StartDialolgue(dS);
        mD.CleanMessage();
        this.enabled = false;
        pM.enabled = false;
        dialogueHandler.endDialogue += EndDialogue;
    }

    private void EndDialogue()
    {
        this.enabled = true;
        pM.enabled = true;
        dialogueHandler.endDialogue -= EndDialogue;
    }
}
