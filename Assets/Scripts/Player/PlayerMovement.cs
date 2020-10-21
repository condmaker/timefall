using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private float moveTime;

    private Rigidbody playerBody;
    private float timeCounter;
    private Vector3 zeroedVector;

    private bool lookUp = false;
    private bool isRotating = false;
    private bool isWalking = false;
    private bool isLookingUp = false;
    private bool isLookingLeft = false;
    private bool isLookingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        timeCounter = moveTime;
        isWalking = false;
        zeroedVector = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (timeCounter == moveTime)
        {
            // Move forward
            if (Input.GetKeyDown("up")) isWalking = true;
            // Look Up
            else if (Input.GetKeyDown("down")) lookUp = true;
            // Rotate Left
            else if (Input.GetKeyDown("left")) isLookingLeft = true;
            // Rotate Right
            else if (Input.GetKeyDown("right")) isLookingRight = true;
        }
    }

    void FixedUpdate()
    {
        if (isWalking && !isLookingUp)
        {
            // Sets the player speed
            playerBody.velocity = transform.forward * (
                moveDistance / moveTime);
            // Ticks down the timer so that the player stops at the desired
            // distance
            timeCounter -= Time.fixedDeltaTime;

            if (timeCounter <= 0) playerBody.velocity = zeroedVector;
        }
        else if (isLookingLeft && !isLookingUp)
        {
            RotatePlayer(y: -90f);
        }
        else if (isLookingRight && !isLookingUp)
        {
            RotatePlayer(y: 90f);
        }
        else if (lookUp)
        {
            if (!isLookingUp)
                RotatePlayer(-30, lUp: true);
            else
                RotatePlayer(30, lUp: false);
        }

        if (timeCounter <= 0)
        {
            isWalking = false;
            isLookingLeft = false;
            isLookingRight = false;
            lookUp = false;
            timeCounter = moveTime;
        }
    }

    private void RotatePlayer(
        float x = 0, float y = 0, float z = 0, bool lUp = false)
    {
        // This doesn't work well-- numbers come out wrong
        playerBody.angularVelocity = (new Vector3(x, y, z)
            * Mathf.Deg2Rad) / moveTime;

        Debug.Log(playerBody.angularVelocity);

        timeCounter -= Time.fixedDeltaTime;

        if (timeCounter <= 0)
        {
            isLookingUp = lUp;
            playerBody.angularVelocity = zeroedVector;
        }
  
    }
}
