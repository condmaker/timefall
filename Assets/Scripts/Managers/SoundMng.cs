using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class that is responsible for managing all sounds and music on a certain
/// scene.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/SoundMng")]
public class SoundMng : ScriptableObject
{
    /// <summary>
    /// The master volume of the whole game.
    /// </summary>
    [SerializeField]
    private AudioMixerGroup master = default;
    public AudioMixerGroup Master => master;

    /// <summary>
    /// A list of Audio Sources that are going to be playing at the scene.
    /// </summary>
    private List<AudioSource> audioSources;

    /// <summary>
    /// A property that defines the current music that is playing on the scene.
    /// </summary>
    public AudioSource CurrentMusic { get; private set; }

    /// <summary>
    /// The starting master volume of the game.
    /// </summary>
    [SerializeField][Range(0,100)]
    private int initialVolume;

    void Awake()
    {
        audioSources = new List<AudioSource>();
        PlayerPrefs.SetInt("Master Volume", initialVolume);

    }

    /// <summary>
    /// Plays a specific music on a scene. Can only play one music at a time.
    /// </summary>
    /// <param name="music">The AudioClip of the music itself.</param>
    /// <param name="loop">A bool that defines whether the song loops or
    /// not.</param>
    public void PlayMusic(
        AudioClip music, bool loop = true)
    {
        // Defines the volume that the audio source is going to play
        float volume = 
            (float)((float)PlayerPrefs.GetInt(
                "Music Volume Real", initialVolume) / 100);

        // Stops the previous music if there is any
        if (CurrentMusic != null && CurrentMusic.isPlaying)
            CurrentMusic.Stop();

        // Defines the AudioSource and plays it at the scene
        AudioSource audioSource = NewSoundObject(Vector3.zero);
        audioSource.clip = music;
        audioSource.volume = volume;
        audioSource.Play();

        // Defines the music loop
        if (loop == true)
            audioSource.loop = true;
        else
            audioSource.loop = false;

        // Make the CurrentMusic property the newly created Audio Source
        CurrentMusic = audioSource;

    }

    /// <summary>
    /// Method that plays a SFX on the scene. There can be multiple of them and
    /// supports 3D audio.
    /// </summary>
    /// <param name="sound">The SFX itself.</param>
    /// <param name="pos">The position of the SFX on the scene.</param>
    /// <param name="spBlend">If the SFX has spatial blend or not (if its 
    /// 3D or not, essentially)</param>
    public void PlaySound(AudioClip sound, Vector3 pos, bool spBlend = false)
    {
        

        // Map decreasing volume. Max decrease is -20 for a smother volume
        // change
        float volume = ((float)(
            ((float)PlayerPrefs.GetInt(
                "SFX Volume Real", initialVolume)) * 20) / 100) - 20;

        // If value is 0 completely mute audio mixer
        if (PlayerPrefs.GetInt("SFX Volume Real") == 0)
            volume = -80;

        // Defines the AudioSource and plays it at the scene
        Master.audioMixer.SetFloat("sfxVol", volume);
        AudioSource audioSource = NewSoundObject(pos);
        audioSource.clip = sound;
        audioSource.volume = 1;
        if (!spBlend)
            audioSource.spatialBlend = 0.8f;
        else
            audioSource.spatialBlend = 0f;
        audioSource.outputAudioMixerGroup = Master;
        audioSource.Play();
    }

    /// <summary>
    /// Instantiantes and destroys AudioSource GameObjects on the scene.
    /// </summary>
    /// <param name="pos">The position of the SFX</param>
    /// <returns>The new SoundObject</returns>
    private AudioSource NewSoundObject(Vector3 pos)
    {
        // Destroys any SoundObject on the scene that is not playing anymore
        foreach (AudioSource audio in audioSources)
        {
            if (audio == null)
                continue;
            else if (!audio.isPlaying)
                Destroy(audio.gameObject);
        }

        // Creates the new SoundObject
        GameObject gObject = new GameObject();
        gObject.name = "SoundEffect";
        gObject.transform.position = pos;
        AudioSource audioSource = gObject.AddComponent<AudioSource>();

        // Adds said SoundObject to the list
        audioSources.Add(audioSource);

        // Returns it
        return audioSource;
    }
}
