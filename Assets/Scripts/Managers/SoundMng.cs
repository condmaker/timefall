using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMng : MonoBehaviour
{
    static public SoundMng instance;
    [SerializeField]
    private AudioMixerGroup master;

    List<AudioSource> audioSources;
    AudioSource currentMusic;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        audioSources = new List<AudioSource>();
    }

    public void PlayMusic(
        AudioClip music, bool loop = true, float volume = 1.0f)
    {
        if (currentMusic != null && currentMusic.isPlaying)
            currentMusic.Stop();

        AudioSource audioSource = NewSoundObject(Vector3.zero);
        audioSource.clip = music;
        audioSource.volume = volume;
        audioSource.Play();

        if (loop == true)
            audioSource.loop = true;
        else
            audioSource.loop = false;

        currentMusic = audioSource;

    }

    public void PlaySound(AudioClip sound, Vector3 pos, float volume = 1.0f)
    {
        AudioSource audioSource = NewSoundObject(pos);
        audioSource.clip = sound;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1f;
        audioSource.outputAudioMixerGroup = master;
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
        gObject.transform.parent = transform;
        AudioSource audioSource = gObject.AddComponent<AudioSource>();

        audioSources.Add(audioSource);

        return audioSource;
    }
}
