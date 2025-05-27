using UnityEngine;

public class AudioFX : MonoBehaviour
{
    public AudioSource audioSource;
    [Range(0f, 1f)]
    public float volume;
    public float pitch;
    public bool isLooping;

    public void PlaySFX(AudioClip[] clip)
    {
        audioSource.PlayOneShot(clip[Random.Range(0,clip.Length)], volume);
    }
}

