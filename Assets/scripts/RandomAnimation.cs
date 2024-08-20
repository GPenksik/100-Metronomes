using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationStateBehaviour : StateMachineBehaviour
{
    public float minSpeed = 0.5f;
    public float maxSpeed = 1.250f;
    public float minStartTime = 0f;
    public float maxStartTime = 0.5f;

    private bool initialized = false;

    // 在动画状态进入时调用
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!initialized)
        {
            // 随机化播放速度
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            animator.speed = randomSpeed;

            // 随机化开始时间
            float randomStartTime = Random.Range(minStartTime, maxStartTime);
            animator.Play(stateInfo.fullPathHash, -1, randomStartTime);

            initialized = true;
        }
    }
}

