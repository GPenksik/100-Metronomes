using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSound : MonoBehaviour
{
    public AudioClip audioClips;
    private AudioSource audioSource;
    private Animator animator;
    private bool audioPlayed = true; // 确保音频只播放一次
    private float previousNormalizedTime = 0f; // 记录上一次的 normalizedTime
    public bool UseAudio;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClips;
        animator = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("AudioGudance", 1) == 1)
            UseAudio = true;
    }

    void Update()
    {
        CheckUseAudio();
    }

    public void CheckUseAudio()
    {

        if (UseAudio)
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

        // 在动画归一化时间达到0.8时播放音频
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
