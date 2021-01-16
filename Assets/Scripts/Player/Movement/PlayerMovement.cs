// TO-DO 
// Fix the bug where looking up on other rotations makes the player look 
// sideways or down

// This iscript must be executed after PlayerInput, due to the bump movement.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float TimeCounter { get; private set; }

    private PlayerInput pI;
    private Rigidbody playerBody;
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
            else if (!pI.IsLookingUp)
            {
                MovePlayer();
            }
            else if (pI.IsLookingUp) pI.IsWalking = false;
        }
        else if (pI.IsLookingLeft && !pI.LookUp)
        {
            RotatePlayer(y: -90f);
        }
        else if (pI.IsLookingRight && !pI.LookUp)
        {
            RotatePlayer(y: 90f);
        }

        if (TimeCounter <= 0)
        {
            pI.IsWalking = false;
            pI.IsStrafingRight = false;
            pI.IsStrafingLeft = false;
            pI.IsLookingLeft = false;
            pI.IsLookingRight = false;
            pI.Bump = false;
            TimeCounter = pI.MoveTime;
        }
    }

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

    private void RotatePlayer(float x = 0, float y = 0, float z = 0)
    {
        playerBody.angularVelocity = (new Vector3(x, y, z)
            * Mathf.Deg2Rad) / pI.MoveTime;

        TimeCounter -= Time.fixedDeltaTime;

        if (TimeCounter <= 0)
            playerBody.angularVelocity = Vector3.zero;
    }
}
