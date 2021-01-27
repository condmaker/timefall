using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages the player's camera movement.
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    /// <summary>
    /// A timer that determines when the camera should stop rotating.
    /// </summary>
    public float TimeCounter { get; private set; }

    /// <summary>
    /// Instance of the Player Input script in order to check what keys the
    /// player has pressed.
    /// </summary>
    private PlayerInput pI;
    /// <summary>
    /// Instance of the player's rigidbody (needed to stop and change angular
    /// velocity)
    /// </summary>
    private Rigidbody playerBody;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        pI = GetComponent<PlayerInput>();
        TimeCounter = pI.MoveTime;
    }

    private void FixedUpdate()
    {
        if (pI.LookUp)
        {
            if (!pI.IsLookingUp)
                RotateCam(-30, lUp: true);
            else
                RotateCam(30, lUp: false);
        }
        if (pI.LookDown)
        {
            if (!pI.IsLookingDown)
                RotateCam(30, lUp: true, rD: true);
            else
                RotateCam(-30, lUp: false, rD: true);
        }

        if (TimeCounter <= 0)
        {
            pI.LookUp = false;
            pI.LookDown = false;

            TimeCounter = pI.MoveTime;
        }
    }

    /// <summary>
    /// Rotates the player.
    /// </summary>
    /// <param name="x">Rotation in euler at the X axis.</param>
    /// <param name="y">Rotation in euler at the Y axis.</param>
    /// <param name="z">Rotation in euler at the Z axis.</param>
    /// <param name="lUp">Bool that defines if the player is looking in
    /// the direction of the rotation or forward.</param>
    /// <param name="rD">Bool that defines if the player is looking up
    /// or down.</param>
    private void RotateCam(
        float x = 0, float y = 0, float z = 0, bool lUp = false, 
        bool rD = false)
    {
        transform.rotation *= Quaternion.Euler(
            (new Vector3(x, y, z) * Mathf.Deg2Rad) / pI.MoveTime);

        TimeCounter -= Time.fixedDeltaTime;

        if (TimeCounter <= 0)
        {
            if (!rD) pI.IsLookingUp = lUp;
            else pI.IsLookingDown = lUp;

            playerBody.angularVelocity = Vector3.zero;
        }

    }
}
