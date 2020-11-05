using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    [SerializeField]
    private GameObject gameobject1, gameobject2, gameobject3;
    private Animator gameobject1Anim, gameobject2Anim, gameobject3Anim;

    private void Awake()
    {
        gameobject1Anim = gameobject1.GetComponent<Animator>();
    }
    private void Update()
    {

    }
    public void Toggle()
    {
        if (gameobject1Anim.GetBool("OpenStatus") == true)
            gameobject1Anim.SetBool("OpenStatus", false);
        else
            gameobject1Anim.SetBool("OpenStatus", true);


    }
}
