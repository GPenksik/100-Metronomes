using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    public bool IsReversed = false;
    private float originalDuration = 0.5f; //原始时长（秒）
    private float targetDuration; // 目标播放时长
    private float Alpha = 1.0f;
    private float time_error = 0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator component not found on" + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnimationSpeed();
    }

    void ChangeAnimationSpeed()
    {
        // 检测空格键按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 记录空格键按下的时间戳
            TimeManager.Instance.AddTimestamp(Time.time);
            if (TimeManager.Instance.GetsatampNum() > 2)
            {
                targetDuration = TimeManager.Instance.GetTimeDifferenceBetweenLastTwoTimestamps();
                time_error = targetDuration - originalDuration;
                //time_error = targetDuration - originalDuration + time_error;
                //targetDuration = time_error * Alpha + originalDuration;
                Debug.Log("target Duration: " + targetDuration);
                UpdateAnimationSpeed();
            }
        }
    }

    void UpdateAnimationSpeed()
    {
        SetAnimationSpeedsign();
        float newSpeed = originalDuration / targetDuration;
        //Debug.Log("target Duration: " + targetDuration);
        SetAnimationSpeed(newSpeed);

    }


    public void SetAnimationSpeed(float speed)
    {
        if (animator != null)
        {
            animator.speed = speed;
            Debug.Log("Animation s00000peed set to: " + speed);
        }
    }

    public float GetAnimationSpeedSign()
    {
        //return animator != null ? animator.speed : 0f;
        if (animator != null)
        { 
            return animator.speed >= 0 ? 1 : -1;
        }
        else
        {
            return 0; // Animator 组件不存在时返回0，表示没有速度
        }
    }

    public void SetAnimationSpeedsign()
    {
        IsReversed = !IsReversed;
        animator.SetBool("IsReversed", IsReversed);    
    }
}
