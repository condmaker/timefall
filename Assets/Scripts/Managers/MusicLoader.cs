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
    private SoundMng soundManager = default;

    /// <summary>
    /// The music intance that'll be played.
    /// </summary>
    [SerializeField]
    private AudioClip music = default;

    /// <summary>
    /// A bool that defines if either the song's gonna be looped or not.
    /// </summary>
    [SerializeField]
    private bool loop;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    private void Awake()
    {
        soundManager?.PlayMusic(music, loop);
    }

}
