using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTimeManager : MonoBehaviour
{
    // 单例实例
    public static UserTimeManager Instance { get; private set; }

    // 存储时间戳的列表
    private List<float> Timestamps = new List<float>();

    private void Awake()
    {
        // 确保只有一个实例
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
    public void AddTimestamp(float time)
    {
        if (Timestamps.Count > 0)
        {
            float lastTime = Timestamps[Timestamps.Count - 1];
            if (time - lastTime < 0.05f)
            {
                //Debug.LogWarning("Timestamp ignored: Less than 0.05 seconds since the last timestamp.");
                return;
            }
        }

        Timestamps.Add(time);
    }

    public List<float> GetClapTimestamps()
    {
        return new List<float>(Timestamps);
    }

    public int GetsatampNum()
    {
        return Timestamps.Count;
    }
    public float GetLatestClapTimestamp()
    {
        if (Timestamps.Count > 0)
        {
            return Timestamps[Timestamps.Count - 1];
        }
        else
        {
            Debug.LogWarning("No space key timestamps recorded yet");
            return -1f;
        }
    }

    public float GetAverageIntervalOfLastFiveSpaceTimestamps()
    {
        if (Timestamps.Count < 5)
        {
            Debug.LogWarning("Not enough space key timestamps to calculate the average interval.");
            return 0f;
        }

        float totalInterval = 0f;
        for (int i = Timestamps.Count - 5; i < Timestamps.Count - 1; i++)
        {
            totalInterval += Timestamps[i + 1] - Timestamps[i];
        }

        return totalInterval / 4f; // 5 timestamps means 4 intervals
    }

    public float GetTimeDifferenceBetweenLastTwoTimestamps()
    {
        if (Timestamps.Count < 3)
        {
            Debug.LogWarning("Not enough timestamps to calculate the time difference.");
            return 0f;
        }

        float lastTimestamp = Timestamps[(Timestamps.Count - 1)];
        //Debug.Log("lastTimestamp: " + Timestamps.Count);
        //Debug.Log("lastTimestamp: " + lastTimestamp);
        float secondLastTimestamp = Timestamps[(Timestamps.Count - 2)];
        //Debug.Log("secondLastTimestamp: " + secondLastTimestamp);
        //Debug.Log("lastTimestamp: " + Timestamps.Count);
        //Debug.Log("Timestamps: " + Timestamps[0]);

        return lastTimestamp - secondLastTimestamp;
    }
}