// THIS SCRIPT **MUST** BE EXECUTED AFTER PLAYERINPUT, DUE TO MOVEMENT.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that makes the player move around the scene.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// A timer that determines when the player should stop moving.
    /// </summary>
    public float TimeCounter { get; private set; }

    /// <summary>
    /// Instance of PlayerInput.
    /// </summary>
    private PlayerInput pI;
    /// <summary>
    /// Instance of the player's Rigidbody. Used for acceleration.
    /// </summary>
    private Rigidbody playerBody;
    /// <summary>
    /// The previous frame position of the player. Used to readjust positions
    /// after bumping into something.
    /// </summary>
    private Vector3 adjustPos;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        pI =         GetComponent<PlayerInput>();

        TimeCounter = pI.MoveTime;
        pI.IsWalking = false;
    }

    private void FixedUpdate()
    {
        if (pI.IsWalking)
        {
            if (pI.Bump)
            {
                MovePlayer(true);
            }
            else if (!pI.IsLookingUp && !pI.IsLookingDown)
            {
                MovePlayer();
            }
            else if (pI.IsLookingUp || pI.IsLookingDown) pI.IsWalking = false;
        }
        else if (pI.IsLookingLeft && (!pI.LookUp || !pI.LookDown))
        {
            RotatePlayer(y: -90f);
        }
        else if (pI.IsLookingRight && (!pI.LookUp || !pI.LookDown))
        {
            RotatePlayer(y: 90f);
        }

        if (TimeCounter <= 0)
        {
            pI.ResetInputs();
            TimeCounter = pI.MoveTime;
        }
    }

    /// <summary>
    /// Method that moves the player. Direction varies on PlayerInput.
    /// </summary>
    /// <param name="bump">Bool that specifies whether the player will
    /// bump into something or not.</param>
    private void MovePlayer(bool bump = false)
    {
        Vector3 uV = new Vector3(0.0f, -1.0f, 0.0f);

        if (pI.IsStrafingRight)
        {
            playerBody.velocity = Quaternion.AngleAxis(90, transform.forward)
                * uV * (pI.MoveDistance / pI.MoveTime);
        }
        else if (pI.IsStrafingLeft)
        {
            playerBody.velocity = Quaternion.AngleAxis(-90, transform.forward)
                * uV * (pI.MoveDistance / pI.MoveTime);
        }
        else if (pI.IsWalkingBack)
        {
            playerBody.velocity = -transform.forward * (
                pI.MoveDistance / pI.MoveTime);
        }
        else
        {
            playerBody.velocity = transform.forward * (
                pI.MoveDistance / pI.MoveTime);
        }

        if (bump)
        {
            if (TimeCounter == pI.MoveTime) adjustPos = transform.position;
            if (TimeCounter <= pI.MoveTime/1.15) playerBody.velocity *= -0.15f;
        }

        TimeCounter -= Time.fixedDeltaTime;

        if (TimeCounter <= 0)
        {
            playerBody.velocity = Vector3.zero;
            //Debug.Log(adjustPos);
            if (bump) transform.position = adjustPos;
        }
    }

    /// <summary>
    /// Rotates the player on a certain direction.
    /// </summary>
    /// <param name="x">Rotation in euler at the X axis.</param>
    /// <param name="y">Rotation in euler at the Y axis.</param>
    /// <param name="z">Rotation in euler at the Z axis.</param>
    private void RotatePlayer(float x = 0, float y = 0, float z = 0)
    {
        playerBody.angularVelocity = (new Vector3(x, y, z)
            * Mathf.Deg2Rad) / pI.MoveTime;

        TimeCounter -= Time.fixedDeltaTime;

        if (TimeCounter <= 0)
            playerBody.angularVelocity = Vector3.zero;
    }
}
