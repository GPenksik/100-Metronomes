using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Asynchrony : MonoBehaviour
{

    public AnimationController Shaft1; // 引用 Shaft1 的脚本
    private float originalDuration = 0.5f; //原始时长（秒）
    private float targetDuration; // 目标播放时长
    private float Alpha = 1.0f;
    private float time_error = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Synchronize();

    }
    void Synchronize()
    {
        // 检测空格键按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 记录空格键按下的时间戳
            TimeManager.Instance.AddTimestamp(Time.time);
            if (TimeManager.Instance.GetsatampNum() > 5)
            {
                targetDuration = TimeManager.Instance.GetTimeDifferenceBetweenLastTwoTimestamps();
                time_error = targetDuration - originalDuration + time_error;
                targetDuration = time_error * Alpha + originalDuration;
                Debug.Log("target Duration: " + targetDuration);
                UpdateAnimationSpeed();
            }
        }
    }

    void UpdateAnimationSpeed()
    {
        float newSpeed = originalDuration / targetDuration;
        //Debug.Log("target Duration: " + targetDuration);
        Shaft1.SetAnimationSpeed(newSpeed);

    }
}
