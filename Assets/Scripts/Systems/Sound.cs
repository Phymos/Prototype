using UnityEngine;

[System.Serializable]
public class Sound
{
    public string clipName;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [Header("Randomization")]
    public bool useRandomPitch = false;
    [Range(0f, 0.5f)]
    public float pitchVariance = 0.1f;

    public bool useRandomVolume = false;
    [Range(0f, 0.5f)]
    public float volumeVariance = 0.05f;

    [HideInInspector]
    public AudioSource source;
}
