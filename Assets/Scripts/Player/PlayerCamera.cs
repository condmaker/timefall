using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float TimeCounter { get; private set; }

    private PlayerInput pI;
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

        if (TimeCounter <= 0)
        {
            pI.LookUp = false;
            TimeCounter = pI.MoveTime;
        }
    }

    // Rotates the player upwards
    private void RotateCam(
        float x = 0, float y = 0, float z = 0, bool lUp = false)
    {
        transform.rotation *= Quaternion.Euler(
            (new Vector3(x, y, z) * Mathf.Deg2Rad) / pI.MoveTime);

        TimeCounter -= Time.fixedDeltaTime;

        if (TimeCounter <= 0)
        {
            pI.IsLookingUp = lUp;
            playerBody.angularVelocity = Vector3.zero;
        }

    }
}
