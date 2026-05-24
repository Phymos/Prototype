using System;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    public void PlaySFX(string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == clipName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        if (s.useRandomPitch)
            s.source.pitch = s.pitch + UnityEngine.Random.Range(-s.pitchVariance, s.pitchVariance);
        else
            s.source.pitch = s.pitch;

        if (s.useRandomVolume)
            s.source.volume = s.volume + UnityEngine.Random.Range(-s.volumeVariance, s.volumeVariance);
        else
            s.source.volume = s.volume;

        s.source.PlayOneShot(s.source.clip);
    }

    public void PlayMusic(string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == clipName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }
}
