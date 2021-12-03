using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
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

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If you want to play the sound. call: FindObjectOfType<AudioManager>().Play("name")
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (name == null)
        {
            Debug.LogWarning("Sound "+name+ " was Not Found!");
            return;
        }

        s.source.Play();
    }

    // If you want to loop play the sound. call: FindObjectOfType<AudioManager>().LoopSound("name")
    public void LoopSound(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        if (name == null)
        {
            Debug.LogWarning("Sound " + name + " was Not Found!");
            return;
        }

        if (!s.source.isPlaying)
        {
            s.source.PlayOneShot(s.clip);
        }
    }

    // If you want to stop the sound from looping. call: FindObjectOfType<AudioManager>().Stop("name")
    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (name == null)
        {
            Debug.LogWarning("Sound " + name + " was Not Found!");
            return;
        }

        s.source.Stop();
    }
}
