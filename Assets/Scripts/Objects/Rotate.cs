using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private float angle;
    [SerializeField]
    private float frames;
    private short previous;

    private float step;

    private bool clockWise;
    private bool counterClockWise;

    private void Awake()
    {
        ObjectStateHandler osh = GetComponent<ObjectStateHandler>();
        previous = osh.State;
        osh.OnChangeState += RotateObject;
    }

    private void RotateObject(ObjectStateHandler osh, short state)
    {
        if(clockWise || counterClockWise)
        {
            osh.State = previous;
            return;
        }

        if ((previous == 0) && (state == osh.MaxStates - 1))
            counterClockWise = true;      
        else if ((previous == osh.MaxStates - 1) && (state == 0))
            clockWise = true;
        else if (previous > state)
            counterClockWise = true;
        else if (previous < state)
            clockWise = true;

        previous = state;
    }

    private void FixedUpdate()
    {
        if (clockWise)
            RotateClockWise();
        else if (counterClockWise)
            RotateCouterClockWise();
    }

    private void RotateClockWise()
    {
        float speed = angle / frames;
        transform.Rotate(new Vector3(0, speed, 0));
        step += speed;
        if (step >= angle) 
        {
            step = 0;
            clockWise = false;
        }
    }

    private void RotateCouterClockWise()
    {
        float speed = angle / frames;
        transform.Rotate(new Vector3(0, -speed, 0));
        step += speed;
        if (step >= angle)
        {
            step = 0;
            counterClockWise = false;
        }
    }
}
