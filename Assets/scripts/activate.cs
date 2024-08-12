using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Activate : MonoBehaviour
{
    private Animator animator;
    public bool Switch = false;
    private float defaultBpm = 120f;
    private float bpm;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
            return;
        }

        // 初始化时读取 BPM 值
        bpm = PlayerPrefs.GetFloat("BPM", defaultBpm);
        UpdateAnimationSpeed();
    }

    // Update is called once per frame

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 实时读取 BPM 值并更新动画速度
        float newBpm = PlayerPrefs.GetFloat("BPM", defaultBpm);
        if (newBpm != bpm)
        {
            bpm = newBpm;
            UpdateAnimationSpeed();
        }

        if (stateInfo.normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            Switch = !Switch;
            animator.SetBool("Switch", Switch);

        }

    }
    void UpdateAnimationSpeed()
    {
        float animationSpeed = bpm / defaultBpm;
        animator.speed = animationSpeed;
    }

}
