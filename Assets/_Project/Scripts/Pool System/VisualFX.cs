using UnityEngine;

public class VisualFX : MonoBehaviour
{
    public ParticleSystem particle;
    public void PlayVFX()
    {
        particle.Play();
    }
}

