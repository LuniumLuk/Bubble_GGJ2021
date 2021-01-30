using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource rocketAudio;
    public AudioSource combainAudio;
    public AudioSource ruptureAudio;
    public AudioSource gasAudio;

    private void Awake()
    {
        instance = this;
    }

}
