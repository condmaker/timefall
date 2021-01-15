using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{
    private Animator wallAnim;

    // Start is called before the first frame update
    private void Start()
    {
        wallAnim = GetComponent<Animator>();
    }


    public void Break()
    {
        wallAnim.SetTrigger("break");
    }
}
