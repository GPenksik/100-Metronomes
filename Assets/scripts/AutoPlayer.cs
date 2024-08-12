using System.Collections.Generic;
using UnityEngine;

public class AutoPlayer : Player
{
    public bool IsReversed = false;
    private List<float> animationSpeeds = new List<float>(); // 用于存储动画速度的列表

    protected override void Start()
    {
        base.Start(); // 调用基类的 Start 方法
        notePlayed = false;
    }

    void Update()
    {
        AnimationEnd();
    }

    private void AnimationEnd()
    {
        AnimatorStateInfo stateInfo = dialAnimator.GetCurrentAnimatorStateInfo(0);

        // 如果动画已经播放完毕，normalizedTime >= 1 表示动画已播放完
        if (stateInfo.normalizedTime >= 1 && !dialAnimator.IsInTransition(0))
        {
            // 记录动画结束时间
            latestOnsetTimes.Add(Time.time);
            SetAnimationSpeedsign();
            ApplyStoredSpeed(); // 在动画结束时应用存储的速度
            PlayNote();
        }
    }

    public override void RecalculateOnsetInterval(float originalAnimationDuration,
                                                  List<Player> players,
                                                  List<float> alphas,
                                                  List<float> betas)
    {
        float alphaSum = 0f;
        float betaSum = 0f;
        float Animationlength = 0.5f;

        for (int i = 0; i < players.Count; ++i)
        {
            float async = GetLatestOnsetTime() - players[i].GetLatestOnsetTime();
            alphaSum += alphas[i] * async;
            //Debug.Log("alphaSum: " + alphaSum);
            betaSum += betas[i] * async;
        }

        Debug.Log("alphaSum: " + alphaSum);
        // 更新时间保持器的平均值
        timeKeeperMean -= betaSum;

        // 生成噪声
        float hNoise = GenerateHNoise();
        //Debug.Log("Noise" + hNoise);


        // 计算下一个开始时间间隔
        onsetInterval = originalAnimationDuration - alphaSum + 0.1f*hNoise;
        Debug.Log("originalAnimationDuration: " + originalAnimationDuration);
        Debug.Log("onsetInterval: " + onsetInterval);

        // 存储计算得到的动画速度

        float calculatedSpeed = Animationlength / onsetInterval;
        //Debug.Log("originalAnimationDuration  " + originalAnimationDuration);
        //Debug.Log("calculatedSpeed: " + calculatedSpeed);
        animationSpeeds.Add(calculatedSpeed);
    }

    public void SetAnimationSpeedsign()
    {
        IsReversed = !IsReversed;
        dialAnimator.SetBool("IsReversed", IsReversed);
    }

    // 应用存储的速度
    public void ApplyStoredSpeed()
    {
        if (animationSpeeds.Count > 0)
        {
            dialAnimator.speed = animationSpeeds[animationSpeeds.Count - 1];
            //Debug.Log("Animation speed set to: " + animationSpeeds[animationSpeeds.Count - 1]);
        }
        else
        {
            dialAnimator.speed = 1f;
        }
    }
}
