using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnly : MonoBehaviour
{
    public AudioClip audioClips;
    private AudioSource audioSource;
    private float defaultBpm = 120f;
    private float bpm;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.pitch = 2f;
        audioSource.clip = audioClips;
        float WaitTime = Updateinterval();

        InvokeRepeating("PlayAudioClip", 0f, WaitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayAudioClip()
    {
        audioSource.Play();
    }
    public float Updateinterval()
    {
        bpm = PlayerPrefs.GetFloat("BPM", defaultBpm);
        float Speed =  defaultBpm / bpm;
        return Speed * 0.5f;
    }
}
