using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleScript : MonoBehaviour
{
    public ParticleSystem particles;

    public void PlayParticleSystem()
    {
        particles.Play();
    }
}
