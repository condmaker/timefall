using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObjects/SoundMng")]
public class SoundMng : ScriptableObject
{
    [SerializeField]
    private AudioMixerGroup master;
    public AudioMixerGroup Master => master;

    List<AudioSource> audioSources;
    public AudioSource CurrentMusic { get; private set; }

    [SerializeField][Range(0,100)]
    private int initialVolume;

    void Awake()
    {
        audioSources = new List<AudioSource>();
        PlayerPrefs.SetInt("Master Volume", initialVolume);

    }

    public void PlayMusic(
        AudioClip music, bool loop = true)
    {

        float volume = 
            (float)((float)PlayerPrefs.GetInt("Music Volume Real", initialVolume) / 100);

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

    public void PlaySound(AudioClip sound, Vector3 pos, bool spBlend = false)
    {
        

        //Map decreasing volume. Max decrease is -20 for a smother volume change
        float volume = ((float)(
            ((float)PlayerPrefs.GetInt("SFX Volume Real", initialVolume)) * 20) / 100)
        - 20;

        //If value is 0 completely mute audio mixer
        if (PlayerPrefs.GetInt("SFX Volume Real") == 0)
            volume = -80;


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
