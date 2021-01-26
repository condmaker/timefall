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

    private PlayerMovement pM;
    private EntityDetection eD;

    [SerializeField]
    private KeyBindings playerBinds;

    [SerializeField]
    private AudioClip stepSound;
    [SerializeField]
    private SoundMng soundManager;

    private bool locke;

    [SerializeField]
    private OptionEvent option;

    public float MoveDistance { get => moveDistance; }
    public float MoveTime { get => moveTime; }

    // Bool that specifies if the player is pressing the interact key
    public bool IsInteracting { get; set; }

    // Movement state bools (we may need to change this because it breaks 
    // encapsulation kinda)

    public bool LookUp { get; set; }
    public bool LookDown { get; set; }
    public bool IsWalking { get; set; }
    public bool IsStrafingRight { get; set; }
    public bool IsStrafingLeft { get; set; }
    public bool Bump { get; set; }
    public bool IsLookingUp { get; set; }
    public bool IsLookingDown { get; set; }
    public bool IsLookingLeft { get; set; }
    public bool IsLookingRight { get; set; }
    public bool IsWalkingBack { get; set; }

    // Bool that specifies if the player can move or not
    public bool CanInput { get; private set; }

    private void Start()
    {
        pM = GetComponent<PlayerMovement>();
        eD = GetComponent<EntityDetection>();

        locke = false;
        option.OptionDisabled += UnlockInput;
        option.OptionEnabled += LockInput;
    }

    private void Update()
    {
        if (locke) return;

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
        if (IsWalking && !IsLookingUp && !IsLookingDown)
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
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(playerBinds.StrafeRight)
            || Input.GetKey(playerBinds.StrafeLeft))
            || Input.GetKey(KeyCode.S))
        {
            if (LookUp || IsLookingUp || LookDown || IsLookingDown)
                return;

            if (!IsWalking && !IsLookingUp && !IsLookingDown)
                soundManager.PlaySound(stepSound, transform.position, true);

            IsWalking = true;

            if (Input.GetKey(playerBinds.StrafeRight))
                IsStrafingRight = true;
            else if (Input.GetKey(playerBinds.StrafeLeft))
                IsStrafingLeft = true;
            else if (Input.GetKey(KeyCode.S))
                IsWalkingBack = true;

        }
        // Look Up and Down
        else if (Input.GetKey(KeyCode.Z))
        {
            if (IsLookingDown || LookDown)
                LookUp = false;
            else
                LookUp = true;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            if (IsLookingUp || LookUp)
                LookDown = false;
            else
                LookDown = true;
        }
        // Rotate Left
        else if (Input.GetKey(playerBinds.RotateLeft) && !IsLookingUp && !LookUp
            && !IsLookingDown && !LookDown)
            IsLookingLeft = true;
        // Rotate Right
        else if (Input.GetKey(playerBinds.RotateRight) && !IsLookingUp && !LookUp
            && !IsLookingDown && !LookDown)
            IsLookingRight = true;
        // Pressed interact key
        else if (Input.GetKeyDown(KeyCode.F)) IsInteracting = true;
    }

    private void CheckInputUp()
    {
        if (Input.GetKeyUp(KeyCode.A) && !IsLookingUp && !LookUp
            && !IsLookingDown && !LookDown)
            IsLookingLeft = false;
        // Rotate Right
        else if (Input.GetKeyUp(KeyCode.D) && !IsLookingUp && !LookUp
            && !IsLookingDown && !LookDown)
            IsLookingRight = false;
        // Pressed interact key
        else if (Input.GetKeyUp(KeyCode.F)) IsInteracting = false;
    }

    public void ResetInputs()
    {
        IsWalking = false;
        IsWalkingBack = false;
        IsStrafingRight = false;
        IsStrafingLeft = false;
        IsLookingLeft = false;
        IsLookingRight = false;
        Bump = false;
    }

    private void LockInput() => locke = true;
    private void UnlockInput() => locke = false;
}
