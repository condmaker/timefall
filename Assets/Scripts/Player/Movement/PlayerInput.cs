using UnityEngine;

/// <summary>
/// Class responsible for obtaining and storing everything related to the
/// Player's input.
/// </summary>
public class PlayerInput : MonoBehaviour
{

    /// <summary>
    /// The movement distance that the player's going to walk (tile-by-tile).
    /// </summary>
    [SerializeField]
    private const float moveDistance = 6;
    /// <summary>
    /// The time that the player takes to walk.
    /// </summary>
    [SerializeField]
    private const float moveTime = 0.3f;

    /// <summary>
    /// Instance of PlayerMovement.
    /// </summary>
    private PlayerMovement pM;
    /// <summary>
    /// Intance of EntityDetection.
    /// </summary>
    private EntityDetection eD;

    /// <summary>
    /// Player Binds that may store some input keys.
    /// </summary>
    [SerializeField]
    private KeyBindings playerBinds = default;

    /// <summary>
    /// Walking SFX.
    /// </summary>
    [SerializeField]
    private AudioClip stepSound = default;
    /// <summary>
    /// Bump SFX.
    /// </summary>
    [SerializeField]
    private AudioClip bumpSound = default;
    /// <summary>
    /// Instance of the SoundManager Scriptable Object.
    /// </summary>
    [SerializeField]
    private SoundMng soundManager = default;

    /// <summary>
    /// Bool that can lock the player from inputting
    /// </summary>
    private bool locke;
    /// <summary>
    /// Bool that defines if that was the first frame that the player bumped
    /// into something
    /// </summary>
    private bool bumpLock;

    /// <summary>
    /// Instance of the current OptionEvent script to launch an event and lock
    /// the player of inputting when he has the options menu open.
    /// </summary>
    [SerializeField]
    private OptionEvent option = default;

    /// <summary>
    /// Property that returns the distance the player moves.
    /// </summary>
    public float MoveDistance { get => moveDistance; }
    /// <summary>
    /// Property that returns the time the player has to move.
    /// </summary>
    public float MoveTime { get => moveTime; }

    /// <summary>
    /// Bool that specifies if the player is pressing the interact key
    /// </summary>
    public bool IsInteracting { get; set; }

    // Movement state bools.
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

    /// <summary>
    /// Bool that specifies if the player can input or not.
    /// </summary>
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

        if (!Bump)
            bumpLock = true;

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

    private void FixedUpdate()
    {
        // Observes if the player's Entity Detection has collided with a World
        // Object that the player cannot pass through. Is on fixed update in
        // order for calculations to be correct

        if (IsWalking && !IsLookingUp && !IsLookingDown)
        {
            // Checks if there is an object in front of the player, 
            // preventing movement

            if (eD.IsColliding && eD.ObjectTouched != null &&
                eD.ObjectTouched.layer != 9)
            {
                Bump = true;

                // It it was the first bump frame, plays the SFX

                if (bumpLock)
                {
                    soundManager.PlaySound(
                        bumpSound, transform.position, true);

                    bumpLock = false;
                }
            }
        }

    }

    /// <summary>
    /// Checks the Forward, Backwards, Strafing, and Up/Down inputs.
    /// </summary>
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
        else if (Input.GetKey(playerBinds.RotateLeft) && !IsLookingUp 
            && !LookUp && !IsLookingDown && !LookDown)
            IsLookingLeft = true;
        // Rotate Right
        else if (Input.GetKey(playerBinds.RotateRight) && !IsLookingUp 
            && !LookUp && !IsLookingDown && !LookDown)
            IsLookingRight = true;
        // Pressed interact key
        else if (Input.GetKeyDown(KeyCode.F)) IsInteracting = true;
    }
    
    /// <summary>
    /// Checks the Camera Rotation and Interaction Inputs.
    /// </summary>
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

    /// <summary>
    /// Resets all player input bools.
    /// </summary>
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

    /// <summary>
    /// Locks the player's inputs.
    /// </summary>
    private void LockInput() => locke = true;
    /// <summary>
    /// Unlocks the player's inputs.
    /// </summary>
    private void UnlockInput() => locke = false;
}
