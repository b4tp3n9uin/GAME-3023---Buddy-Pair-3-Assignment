using UnityEngine.Audio;
using UnityEngine;

/*
 * References:
 * 
 * Brackeys. (2017, May 31). Introduction to AUDIO in Unity. YouTube. https://www.youtube.com/watch?v=6OT43pvUyfY
 * 
 */

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
