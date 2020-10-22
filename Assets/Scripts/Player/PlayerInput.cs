using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement pm;
    private PlayerCamera   pc;

    // May become readonly later for performance purposes, support variables
    // for move distance and time wasted on each step/rotation
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveTime;

    public float MoveDistance { get => moveDistance; }
    public float MoveTime { get => moveTime; }

    // Movement state bools
    public bool LookUp         { get; set; }
    public bool IsWalking      { get; set; }
    public bool Bump           { get; set; }
    public bool IsLookingUp    { get; set; }
    public bool IsLookingLeft  { get; set; }
    public bool IsLookingRight { get; set; }
    public Vector3 ZeroedVector { get; } = new Vector3(0.0f, 0.0f, 0.0f);

    // Bool that specifies if the player can move or not
    private bool canInput;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pc = GetComponent<PlayerCamera>();
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
            if (Input.GetKeyDown("up"))
            {
                IsWalking = true;

                // Checks if there is an object in front of the player, 
                // preventing movement
                if (Physics.Raycast(
                    transform.position, transform.forward, MoveDistance))
                    Bump = true;
            }
            // Look Up
            else if (Input.GetKeyDown("down")) LookUp = true;
            // Rotate Left
            else if (Input.GetKeyDown("left")) IsLookingLeft = true;
            // Rotate Right
            else if (Input.GetKeyDown("right")) IsLookingRight = true;
        }
    }
}
