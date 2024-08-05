using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeManager : MonoBehaviour
{
    // 单例实例
    public static PlayerTimeManager Instance { get; private set; }

    // 存储时间戳的列表
    private List<float> animationEndTimes = new List<float>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 添加时间戳的方法
    public void AddAnimationEndTime(float time)
    {
        if (animationEndTimes.Count > 0)
        {
            float lastTime = animationEndTimes[animationEndTimes.Count - 1];
            if (time - lastTime < 0.05f)
            {
                //Debug.LogWarning("animationEndTimes ignored: Less than 0.05 seconds since the last timestamp.");
                return;
            }
        }
        animationEndTimes.Add(time);
    }

    public List<float> GetClapTimestamps()
    {
        return new List<float>(animationEndTimes);
    }

    public int GetsatampNum()
    {
        return animationEndTimes.Count;
    }
    public float GetLatestClapTimestamp()
    {
        if (animationEndTimes.Count > 0)
        {
            return animationEndTimes[animationEndTimes.Count - 1];
        }
        else
        {
            Debug.LogWarning("No space key timestamps recorded yet");
            return -1f;
        }
    }

    public float GetAverageIntervalOfLastFiveSpaceTimestamps()
    {
        if (animationEndTimes.Count < 5)
        {
            Debug.LogWarning("Not enough space key timestamps to calculate the average interval.");
            return 0f;
        }

        float totalInterval = 0f;
        for (int i = animationEndTimes.Count - 5; i < animationEndTimes.Count - 1; i++)
        {
            totalInterval += animationEndTimes[i + 1] - animationEndTimes[i];
        }

        return totalInterval / 4f; // 5 timestamps means 4 intervals
    }

    public float GetTimeDifferenceBetweenLastTwoTimestamps()
    {
        if (animationEndTimes.Count < 3)
        {
            Debug.LogWarning("Not enough timestamps to calculate the time difference.");
            return 0f;
        }

        float lastTimestamp = animationEndTimes[(animationEndTimes.Count - 1)];
        float secondLastTimestamp = animationEndTimes[(animationEndTimes.Count - 2)];


        return lastTimestamp - secondLastTimestamp;
    }
}