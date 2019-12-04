using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioRandomizer : MonoBehaviour
{
    public AudioSource source;
    public float pitchRange;
    public float standardPitch;

    public void Start()
    {
        standardPitch = source.pitch;
    }

    public void PlaySound(AudioClip[] clips)
    {
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        source.pitch = standardPitch + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip)
    {
        source.pitch = standardPitch + Random.Range(-pitchRange, pitchRange);
        source.PlayOneShot(clip);
    }
}
