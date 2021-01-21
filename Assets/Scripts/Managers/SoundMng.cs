using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "SoundMng")]
public class SoundMng : ScriptableObject
{
    [SerializeField]
    private AudioMixerGroup master;
    public AudioMixerGroup Master => master;

    List<AudioSource> audioSources;
    public AudioSource CurrentMusic { get; private set; }

    void Awake()
    {
        audioSources = new List<AudioSource>();
    }

    public void PlayMusic(
        AudioClip music, bool loop = true, float volume = 1.0f)
    {
        if (CurrentMusic != null && CurrentMusic.isPlaying)
            CurrentMusic.Stop();

        AudioSource audioSource = NewSoundObject(Vector3.zero);
        audioSource.clip = music;
        audioSource.volume = volume;
        audioSource.Play();

        if (loop == true)
            audioSource.loop = true;
        else
            audioSource.loop = false;

        CurrentMusic = audioSource;

    }

    public void PlaySound(AudioClip sound, Vector3 pos, float volume = 1.0f)
    {
        AudioSource audioSource = NewSoundObject(pos);
        audioSource.clip = sound;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1f;
        audioSource.outputAudioMixerGroup = Master;
        audioSource.Play();
    }

    private AudioSource NewSoundObject(Vector3 pos)
    {

        foreach (AudioSource audio in audioSources)
        {
            if (audio == null)
                continue;
            else if (!audio.isPlaying)
                Destroy(audio.gameObject);
        }

        GameObject gObject = new GameObject();
        gObject.name = "SoundEffect";
        gObject.transform.position = pos;
        AudioSource audioSource = gObject.AddComponent<AudioSource>();

        audioSources.Add(audioSource);

        return audioSource;
    }
}
