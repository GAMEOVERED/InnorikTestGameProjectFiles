using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuEnvironment, bookOpen, pageFlip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menuEnvironment;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void playBookOpen()
    {
        audioSource.PlayOneShot(bookOpen);
    }

    public void playBookPageFlip()
    {
        audioSource.PlayOneShot(pageFlip);
    }
}
