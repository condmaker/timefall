using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using DialogueSystem;

/// <summary>
/// Class that is responsible for detecting entities and doing actions
/// depending on what that entity is.
/// </summary>
public class EntityDetection : MonoBehaviour
{
    /// <summary>
    /// Instance of the Player Input script in order to check what keys the
    /// player has pressed.
    /// </summary>
    private PlayerInput pI;
    /// <summary>
    /// Instance of the Player Movement script.
    /// </summary>
    private PlayerMovement pM;

    /// <summary>
    /// Instance of the Player's inventory.
    /// </summary>
    [SerializeField]
    private InventoryHandler inventory;

    /// <summary>
    /// Instance of the Dialogue Handler.
    /// </summary>
    [SerializeField]
    private DialogueDisplayHandler dialogueHandler;

    /// <summary>
    /// Variable that stores the GameObject that the player collided with.
    /// </summary>
    public GameObject ObjectTouched { get; private set; }

    /// <summary>
    /// The Message Display instance in order to show symbols depending on
    /// the object type.
    /// </summary>
    [SerializeField]
    private MessageDisplay mD;
    /// <summary>
    /// Object data of the object the player collided with.
    /// </summary>
    private ObjectData objectData;
    /// <summary>
    /// Data Holder of the object the player collided with.
    /// </summary>
    private DataHolder objectHolder;
    /// <summary>
    /// Interactor of the object the player collided with.
    /// </summary>
    private ManualInteractor interactor;

    /// <summary>
    /// A DialogueScript that plays if the player tries to change an object's
    /// state with an wrong item.
    /// </summary>
    [SerializeField]
    private DialogueScript wrongInteaction;

    /// <summary>
    /// Bool that specifies is the player is colliding with an object.
    /// </summary>
    public bool IsColliding { get; private set; }
    /// <summary>
    /// The Raycast information of the object that the player collided with.
    /// </summary>
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

    /// <summary>
    /// Begins showing a dialogue captured on specific World Objects.
    /// </summary>
    /// <param name="dS">The Dialogue Script of said object.</param>
    private void StartDialogue(DialogueScript dS)
    {
        dialogueHandler.StartDialolgue(dS);
        mD.CleanMessage();
        this.enabled = false;
        pM.enabled = false;
        dialogueHandler.endDialogue += EndDialogue;
    }

    /// <summary>
    /// Ends a dialogue screen.
    /// </summary>
    private void EndDialogue()
    {
        this.enabled = true;
        pM.enabled = true;
        dialogueHandler.endDialogue -= EndDialogue;
    }
}
