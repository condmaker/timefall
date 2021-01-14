﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We need to change all of this to namespaces and use it directly instead of
// getting multiple instances of scripts.
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement  pM;
    private EntityDetection eD;

    [SerializeField]
    private AudioClip stepSoundA;
    [SerializeField]
    private AudioClip stepSoundB;
    [SerializeField]
    private SoundMng soundManager;

    private bool soundSwitch;

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
    // encapsulation kinda)
    private bool lookUp;
    private bool isWalking;
    private bool bump;
    private bool isLookingUp;
    private bool isLookingLeft;
    private bool isLookingRight;

    public bool LookUp         { get => lookUp; set => lookUp = value; }
    public bool IsWalking      { get => isWalking; set => isWalking = value; }
    public bool Bump           { get => bump; set => bump = value; }
    public bool IsLookingUp    { get => isLookingUp; 
                                 set => isLookingUp = value; }
    public bool IsLookingLeft  { get => isLookingLeft;
                                 set => isLookingLeft = value; }
    public bool IsLookingRight { get => isLookingRight; 
                                 set => isLookingRight = value; }

    // Bool that specifies if the player can move or not
    public bool CanInput { get; private set; }

    private void Start()
    {
        pM = GetComponent<PlayerMovement>();
        eD = GetComponent<EntityDetection>();
    }

    private void Update()
    {
        if (pM.TimeCounter == MoveTime)
            CanInput = true;
        else
            CanInput = false;

        if (CanInput)
        {
            // Move forward
            if (Input.GetKey(KeyCode.W) && !LookUp && !isLookingUp)
            {
                if (soundSwitch && !IsWalking)
                    soundManager.PlaySound(stepSoundA, transform.position);
                else if (!soundSwitch && !IsWalking)
                    soundManager.PlaySound(stepSoundB, transform.position);
                soundSwitch = !soundSwitch;

                IsWalking = true;
            }
            // Look Up
            else if (Input.GetKey(KeyCode.S)) LookUp = true;
            // Rotate Left
            else if (Input.GetKey(KeyCode.A) && !IsLookingUp && !LookUp) 
                IsLookingLeft = true;
            // Rotate Right
            else if (Input.GetKey(KeyCode.D) && !IsLookingUp && !LookUp) 
                IsLookingRight = true;
            // Pressed interact key
            else if (Input.GetKey(KeyCode.E)) IsInteracting = true;  
        }
    }

    // This is here so that the fixed update on player movement doesn't bug out
    private void FixedUpdate()
    {
        if (IsWalking)
        {
            // Checks if there is an object in front of the player, 
            // preventing movement
            if (eD.IsColliding && eD.ObjectTouched != null &&
                eD.ObjectTouched.layer != 9)
                Bump = true;
        }
            
    }
}