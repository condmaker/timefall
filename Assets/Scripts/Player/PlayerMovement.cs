// TO-DO 
// Fix the bug where looking up on other rotations makes the player look 
// sideways or down

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
    private Animator playerAnim;
    private float timeCounter;
    private Vector3 zeroedVector;
    private Vector3 adjustPos;

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
        playerAnim = GetComponent<Animator>();

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
        if (isWalking)
        {
            if (Physics.Raycast(transform.position, transform.forward, 4))
            {
                MovePlayer(true);
            }
            else if (!isLookingUp)
            {
                MovePlayer();
            }
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

    private void MovePlayer(bool bump = false)
    {
        playerBody.velocity = transform.forward * (
                moveDistance / moveTime);

        if (bump)
        {
            if (timeCounter == moveTime) adjustPos = transform.position;
            if (timeCounter <= moveTime / 2) playerBody.velocity *= -1;
        }

        timeCounter -= Time.fixedDeltaTime;

        if (timeCounter <= 0)
        {
            playerBody.velocity = zeroedVector;
            if (bump) transform.position = adjustPos;
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
