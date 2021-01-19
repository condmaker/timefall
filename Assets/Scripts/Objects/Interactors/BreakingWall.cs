using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{
    private Animator wallAnim;

    [SerializeField]
    private AudioClip sound;
    [SerializeField]
    private SoundMng soundManager;


    // Start is called before the first frame update
    private void Start()
    {
        wallAnim = GetComponent<Animator>();
    }


    public void Break()
    {
        soundManager.PlaySound(sound, transform.position);
        wallAnim.SetTrigger("break");
    }
}
