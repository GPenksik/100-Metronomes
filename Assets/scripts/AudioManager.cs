using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<AudioClip> audioClips; // 用于管理多个音频片段
    private AudioSource audioSource; // 用于播放音频的AudioSource

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 添加AudioSource组件
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayRandomAudioClip()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            AudioClip clip = audioClips[randomIndex];

            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void PlaySpecificAudioClip(int index)
    {
        if (index >= 0 && index < audioClips.Count)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }
}

