using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We need to change all of this to namespaces and use it directly instead of
// getting multiple instances of scripts.
public class PlayerInput : MonoBehaviour
{

    [SerializeField]
    private const float moveDistance = 6;
    [SerializeField]
    private const float moveTime = 0.3f;

    private PlayerMovement  pM;
    private EntityDetection eD;

    [SerializeField]
    private AudioClip stepSoundA;
    [SerializeField]
    private AudioClip stepSoundB;
    [SerializeField]
    private SoundMng soundManager;

    private bool soundSwitch;

    public float MoveDistance { get => moveDistance; }
    public float MoveTime { get => moveTime; }

    // Bool that specifies if the player is pressing the interact key
    public bool IsInteracting  { get; set; }

    // Movement state bools (we may need to change this because it breaks 
    // encapsulation kinda)

    public bool LookUp          { get; set; }
    public bool IsWalking       { get; set; }
    public bool IsStrafingRight { get; set; }
    public bool IsStrafingLeft  { get; set; }
    public bool Bump            { get; set; }
    public bool IsLookingUp     { get; set; }
    public bool IsLookingLeft   { get; set; }
    public bool IsLookingRight  { get; set; }

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
            // Checks if keys are pressed down
            CheckInputDown();
            // Checks if keys have been pressed up
            CheckInputUp();
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

    private void CheckInputDown()
    {
        // Move forward
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E) ||
            Input.GetKey(KeyCode.Q)) && !LookUp && !IsLookingUp)
        {
            if (soundSwitch && !IsWalking)
                soundManager.PlaySound(stepSoundA, transform.position);
            else if (!soundSwitch && !IsWalking)
                soundManager.PlaySound(stepSoundB, transform.position);
            soundSwitch = !soundSwitch;

            IsWalking = true;

            if (Input.GetKey(KeyCode.E))
                IsStrafingRight = true;
            else if (Input.GetKey(KeyCode.Q))
                IsStrafingLeft = true;
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
        else if (Input.GetKeyDown(KeyCode.F)) IsInteracting = true;
    }

    private void CheckInputUp()
    {
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.E) ||
                Input.GetKeyUp(KeyCode.Q)) && !LookUp && !IsLookingUp)
        {
            if (soundSwitch && !IsWalking)
                soundManager.PlaySound(stepSoundA, transform.position);
            else if (!soundSwitch && !IsWalking)
                soundManager.PlaySound(stepSoundB, transform.position);
            soundSwitch = !soundSwitch;

            IsWalking = true;

            if (Input.GetKey(KeyCode.E))
                IsStrafingRight = true;
            else if (Input.GetKey(KeyCode.Q))
                IsStrafingLeft = true;
        }
        // Look Up
        else if (Input.GetKeyUp(KeyCode.S)) LookUp = false;
        // Rotate Left
        else if (Input.GetKeyUp(KeyCode.A) && !IsLookingUp && !LookUp)
            IsLookingLeft = false;
        // Rotate Right
        else if (Input.GetKeyUp(KeyCode.D) && !IsLookingUp && !LookUp)
            IsLookingRight = false;
        // Pressed interact key
        else if (Input.GetKeyUp(KeyCode.F)) IsInteracting = false;
    }
}
