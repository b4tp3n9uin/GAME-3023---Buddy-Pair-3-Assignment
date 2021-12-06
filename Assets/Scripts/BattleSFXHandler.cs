using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSFXHandler : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> soundEffectClips;
    [SerializeField]
    private int availableSources = 5;

    [SerializeField]
    private Queue<AudioSource> soundEffectsSources = new Queue<AudioSource>();


    // Start is called before the first frame update
    void Start()
    {
        soundEffectsSources = new Queue<AudioSource>();

        for (int i = 0; i < availableSources; i++)
        {
            AudioSource AS = gameObject.AddComponent<AudioSource>();

            AS.loop = false;
            AS.playOnAwake = false;
            AS.volume = 0.6f;

            soundEffectsSources.Enqueue(AS);
        }
    }

    public void PlaySFX(int index)
    {
        // If we have sound effect sources, and our index is within the sound effect clip list...
        if (soundEffectsSources.Count > 0 && index >= 0 && index < soundEffectClips.Count)
        {
            // Get the Audio Source
            AudioSource AS = soundEffectsSources.Dequeue();

            if (AS != null)
            {
                // Stop the audio source if its playing
                AS.Stop();

                // Set the audio source's clip as our clip at the index
                AS.clip = soundEffectClips[index];

                // Play the audio
                AS.Play();
            }

            // Put the audio source back into the queue
            soundEffectsSources.Enqueue(AS);
        }
    }

}
