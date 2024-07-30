using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioPlayer : MonoBehaviour
{
    public AudioClip audioClip; // 在第15帧和第30帧播放的音频
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // 在第15帧和第30帧调用的函数
    public void PlayAudio()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}

