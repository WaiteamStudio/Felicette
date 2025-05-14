 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comicplayaudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip1;
    public AudioClip clip2;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio1()
    {
        if (audioSource != null)
        {
            audioSource.volume = 1f;
            audioSource.clip = clip1;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource!");
        }
    }
    public void PlayAudio2()
    {
        if (audioSource != null)
        {
            audioSource.volume = 0.5f;
            audioSource.clip = clip2;
            audioSource.Play();
            audioSource.volume = 1f;
        }
        else
        {
            Debug.LogWarning("No AudioSource!");
        }
    }
}
