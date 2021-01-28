using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads a selected song when the scene starts.
/// </summary>
public class MusicLoader : MonoBehaviour
{
    /// <summary>
    /// The Sound Manager ScriptableObject that'll play the song.
    /// </summary>
    [SerializeField]
    private SoundMng soundManager;

    /// <summary>
    /// The music intance that'll be played.
    /// </summary>
    [SerializeField]
    private AudioClip music;

    /// <summary>
    /// A bool that defines if either the song's gonna be looped or not.
    /// </summary>
    [SerializeField]
    private bool loop;

    private void Awake()
    {
        soundManager?.PlayMusic(music, loop);
    }

}
