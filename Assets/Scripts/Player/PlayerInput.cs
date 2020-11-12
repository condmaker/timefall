using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We need to change all of this to namespaces and use it directly instead of
// getting multiple instances of scripts.
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement  pm;
    private PlayerCamera    pc;
    private EntityDetection ed;

    // May become readonly later for performance purposes, support variables
    // for move distance and time wasted on each step/rotation
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveTime;

    public float MoveDistance { get => moveDistance; }
    public float MoveTime { get => moveTime; }

    // Bool that specifies if the player is pressing the interact key
    public bool IsInteracting  { get; set; }

    // Movement state bools (we may need to change this because it breaks 
    // encapsulation
    public bool LookUp         { get; set; }
    public bool IsWalking      { get; set; }
    public bool Bump           { get; set; }
    public bool IsLookingUp    { get; set; }
    public bool IsLookingLeft  { get; set; }
    public bool IsLookingRight { get; set; }

    // Bool that specifies if the player can move or not
    private bool canInput;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pc = GetComponent<PlayerCamera>();
        ed = GetComponent<EntityDetection>();
    }

    void Update()
    {
        if (pm.TimeCounter == MoveTime && pc.TimeCounter == MoveTime)
            canInput = true;
        else
            canInput = false;

        if (canInput)
        {
            // Move forward
            if (Input.GetKey("up"))
            {
                IsWalking = true;

                // Checks if there is an object in front of the player, 
                // preventing movement
                if (ed.IsColliding && ed.ObjectTouched != null && 
                    ed.ObjectTouched.layer != 9)
                    Bump = true;
            }
            // Look Up
            else if (Input.GetKey("down")) LookUp = true;
            // Rotate Left
            else if (Input.GetKey("left") && !IsLookingUp) 
                IsLookingLeft = true;
            // Rotate Right
            else if (Input.GetKey("right") && !IsLookingUp) 
                IsLookingRight = true;
            // Pressed interact key
            else if (Input.GetKey("e")) IsInteracting = true;  
        }
    }

    public bool IsStopped()
    {
        if (!IsWalking && !IsLookingUp && !IsLookingLeft && !IsLookingRight)
            return true;
        return false;
    }
}
