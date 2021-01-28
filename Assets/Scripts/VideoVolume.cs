using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Automatically sets the current Video volume to the master volume.
/// </summary>
public class VideoVolume : MonoBehaviour
{
    /// <summary>
    /// Video instance to change the audio volume.
    /// </summary>
    [SerializeField]
    private VideoPlayer video = default;

    /// <summary>
    /// Method called when the object is created
    /// </summary>
    private void Awake() =>
        video.SetDirectAudioVolume(0,
            (float) PlayerPrefs.GetInt("Master Volume") / 100);
}
