using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{

    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0, 1)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
