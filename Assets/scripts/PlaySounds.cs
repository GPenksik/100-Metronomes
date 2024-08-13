using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{

    private Animator animator;
    private bool audioPlayed = true; // 确保音频只播放一次
    private float previousNormalizedTime = 0f; // 记录上一次的 normalizedTime

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = stateInfo.normalizedTime;

        // 检测动画循环的开始（normalizedTime 从 1 回到 0）
        if (currentNormalizedTime < previousNormalizedTime)
        {
            audioPlayed = false; // 重置 audioPlayed
        }

        // 在动画归一化时间达到0.8时播放音频
        if (currentNormalizedTime >= 0.8f && !audioPlayed)
        {
            PlayAudio();
            audioPlayed = true; // 确保音频只播放一次
        }

        // 更新 previousNormalizedTime
        previousNormalizedTime = currentNormalizedTime;
    }

    public void PlayAudio()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayRandomAudioClip();
        }
    }
}
