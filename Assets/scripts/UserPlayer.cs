using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{

    protected override void Start()
    {
        base.Start(); // 调用基类的 Start 方法
        notePlayed = false;
    }

    void Update()
    {
        if (InputChecker.IsTouchBegan())
        {
            float currentTime = Time.time;
            AddTimestamp(currentTime);
            if (latestOnsetTimes.Count > 1)
            {
                onsetInterval = currentTime - latestOnsetTimes[latestOnsetTimes.Count - 2];
            }
            PlayNote();
        }
    }

    public override void RecalculateOnsetInterval(float originalAnimationDuration,
                                                  List<Player> players,
                                                  List<float> alphas,
                                                  List<float> betas)
    {
        float meanOnset = 0.0f;
        float meanInterval = 0.0f;
        int nOtherPlayers = 0;

        for (int i = 0; i < players.Count; ++i)
        {
            if (players[i] != this)
            {
                meanOnset += players[i].GetLatestOnsetTime();
                meanInterval += players[i].onsetInterval;
                ++nOtherPlayers;
            }
        }

        if (nOtherPlayers > 0)
        {
            meanOnset /= nOtherPlayers;
            meanInterval /= nOtherPlayers;
        }
        else
        {
            onsetInterval = meanOnset - GetLatestOnsetTime() + 1.5f * meanInterval;
        }

        // 将最新值存储在PlayerPrefs中
        PlayerPrefs.SetFloat("meanOnset", meanOnset);
        PlayerPrefs.SetFloat("meanInterval", meanInterval);
        PlayerPrefs.SetFloat("onsetInterval", onsetInterval);
        PlayerPrefs.Save(); // 保存更改
    }
}
