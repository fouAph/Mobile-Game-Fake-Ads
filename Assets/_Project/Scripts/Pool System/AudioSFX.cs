using UnityEngine;

public class AudioSFX : MonoBehaviour
{
    public AudioSource audioSource;
    [Range(0f, 1f)]
    public float volume;
    public float pitch;
    public bool isLooping;

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}

