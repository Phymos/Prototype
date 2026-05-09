using UnityEngine;

public class PlayerMovementSfx : MonoBehaviour
{
    private ThirdPersonController playerController;
    public AudioSource audioSource;
    public AudioClip[] grassWalkClips;
    public AudioClip[] grassRunClips;
    public AudioClip[] tileWalkClips;
    public AudioClip[] tileRunClips;
    AudioClip[] walkClips;
    AudioClip[] runClips;

    public AudioClip grassJumpClip;
    public AudioClip tileJumpClip;
    public AudioClip grassLandClip;
    public AudioClip tileLandClip;
    public AudioClip rollClip;
    AudioClip jumpClip;
    AudioClip landClip;

    void Awake()
    {
        playerController = GetComponentInParent<ThirdPersonController>();
        audioSource = GetComponent<AudioSource>();

        walkClips = grassWalkClips;
        runClips = grassRunClips;
        jumpClip = grassJumpClip;
        landClip = grassLandClip;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            if (hit.collider.gameObject.layer == 10) // grass
            {
                walkClips = grassWalkClips;
                runClips = grassRunClips;
                jumpClip = grassJumpClip;
                landClip = grassLandClip;
            }else if (hit.collider.gameObject.layer == 11) // tile
            {
                walkClips = tileWalkClips;
                runClips = tileRunClips;
                jumpClip = tileJumpClip;
                landClip = tileLandClip;
            }
        }
    }

    void PlayFootstep()
    {
        if (walkClips.Length == 0 || runClips.Length == 0)
            {
                return;
            }

        if (playerController.isMoving == true)
            {
                AudioClip[] clips = playerController.isRunning ? runClips : walkClips;

                audioSource.pitch = Random.Range(0.9f, 1.1f);
                float volume = Random.Range(0.7f, 0.8f);
                
                AudioClip clip = clips[Random.Range(0, clips.Length)];
                audioSource.PlayOneShot(clip, volume);
            }
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    public void PlayLandSound()
    {
        audioSource.PlayOneShot(landClip);
    }

    public void PlayRollSound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
            float volume = Random.Range(0.4f, 0.6f);
                
        audioSource.PlayOneShot(rollClip, volume);
    }

    public void PlaySidestepSound()
    {
        // Implement sidestep sound logic here
    }
}