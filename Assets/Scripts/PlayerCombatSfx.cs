using UnityEngine;

public class PlayerCombatSfx : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;

    [Header("Combat SFX")]
    public AudioClip[] lightAttackClips;
    public AudioClip[] heavyAttackClips;
    public AudioClip[] blockClips;
    public AudioClip[] parryClips;
    public AudioClip[] takeDamageClips;
    public AudioClip[] deathClips;

    AudioClip[] clips;

    public void PlayLightAttackSound()
    {
        // Play light attack sound here
        if (lightAttackClips.Length == 0)
            return;

        clips = lightAttackClips;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        float volume = Random.Range(0.7f, 0.8f);
        
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayHeavyAttackSound()
    {
        if (heavyAttackClips.Length == 0)
            return;

        clips = heavyAttackClips;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        float volume = Random.Range(0.7f, 0.8f);

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayBlockSound()
    {
        if (blockClips.Length == 0)
            return;

        clips = blockClips;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        float volume = Random.Range(0.7f, 0.8f);

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayParrySound()
    {
        if (parryClips.Length == 0)
            return;

        clips = parryClips;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        float volume = Random.Range(0.7f, 0.8f);

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayTakeDamageSound()
    {
        if (takeDamageClips.Length == 0)
            return;

        clips = takeDamageClips;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        float volume = Random.Range(0.7f, 0.8f);

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayDeathSound()
    {
        if (deathClips.Length == 0)
            return;

        clips = deathClips;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        float volume = Random.Range(0.7f, 0.8f);

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }
}