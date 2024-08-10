using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAnimation : MonoBehaviour
{
    private Animator animator;
    public bool IsReversed = false;
    private float originalDuration = 0.5f; //原始时长（秒）
    private float targetDuration; // 目标播放时长

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
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
            UserTimeManager.Instance.AddTimestamp(Time.time);
            SetAnimationSpeedsign();
            if (UserTimeManager.Instance.GetsatampNum() > 2)
            {
                targetDuration = UserTimeManager.Instance.GetTimeDifferenceBetweenLastTwoTimestamps();
                //time_error = targetDuration - originalDuration;
                //time_error = targetDuration - originalDuration + time_error;
                //targetDuration = time_error * Alpha + originalDuration;
                //Debug.Log("target Duration: " + targetDuration);
                UpdateAnimationSpeed();
            }

        }
    }

    void UpdateAnimationSpeed()
    {
        float newSpeed = originalDuration / targetDuration;
        //Debug.Log("target Duration: " + targetDuration);
        SetAnimationSpeed(newSpeed);

    }


    public void SetAnimationSpeed(float speed)
    {
        if (animator != null)
        {
            animator.speed = speed;
            //Debug.Log("Animation speed set to: " + speed);
        }
    }

    public void SetAnimationSpeedsign()
    {
        IsReversed = !IsReversed;
        animator.SetBool("IsReversed", IsReversed);
    }
}

