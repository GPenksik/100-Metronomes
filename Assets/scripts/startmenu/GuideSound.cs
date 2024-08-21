using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSound : MonoBehaviour
{
    public AudioClip audioClips;
    private AudioSource audioSource;
    private Animator animator;
    private bool audioPlayed = false; // 确保音频只播放一次
    private float previousNormalizedTime = 0f; // 记录上一次的 normalizedTime
    public int UseAudio;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.pitch = 2f;
        audioSource.clip = audioClips;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UseAudio = PlayerPrefs.GetInt("AudioGudance", 1);
        CheckUseAudio();
    }

    public void CheckUseAudio()
    {

        if (UseAudio==1)
        {
            Checkanimation();
        }
    }
    public void Checkanimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = stateInfo.normalizedTime;

        // 检测动画循环的开始（normalizedTime 从 1 回到 0）
        if (currentNormalizedTime < previousNormalizedTime)
        {
            audioPlayed = false; // 重置 audioPlayed
        }

        // 在动画归一化时间达到0.9时播放音频
        if (currentNormalizedTime >= 0.9f && !audioPlayed)
        {
            PlayAudio();
            audioPlayed = true; // 确保音频只播放一次
        }

        // 更新 previousNormalizedTime
        previousNormalizedTime = currentNormalizedTime;
    }
    public void PlayAudio()
    {
        if (audioSource != null && audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
