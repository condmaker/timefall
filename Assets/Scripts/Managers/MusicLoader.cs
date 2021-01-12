using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    [SerializeField]
    private SoundMng soundManager;

    [SerializeField]
    private AudioClip music;

    [SerializeField]
    private bool loop;

    private void Awake()
    {
        soundManager?.PlayMusic(music, loop);
    }

}
