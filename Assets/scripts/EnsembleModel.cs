using System.Collections.Generic;
using UnityEngine;

public class EnsembleModel : MonoBehaviour
{
    public static EnsembleModel Instance { get; private set; }
    public List<Player> players = new List<Player>();
    public List<List<float>> alphaParams = new List<List<float>>();
    public List<List<float>> betaParams = new List<List<float>>();
    public float originalAnimationDuration = 0.5f; // 示例值，需要根据实际情况设置
    private bool initialTempoSet = false;
    private int scoreCounter = 0;

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

    private void Start()
    {
        // 清空 players 列表
        players.Clear();
        alphaParams.Clear();
        betaParams.Clear();
        scoreCounter = 0;

        // 查找场景中的所有 Player 对象并添加到列表中
        Player[] foundPlayers = FindObjectsOfType<Player>();
        players.AddRange(foundPlayers);

        // 初始化 alphaParams 和 betaParams 列表
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Index = i; // 设置每个玩家的索引
            alphaParams.Add(new List<float>());
            betaParams.Add(new List<float>());

            for (int j = 0; j < players.Count; j++)
            {
                if (i == j)
                {
                    // 自己对自己的 alpha 和 beta 为 0
                    alphaParams[i].Add(0.0f);
                    betaParams[i].Add(0.0f);
                }
                else if (players[i] is UserPlayer || players[j] is UserPlayer)
                {
                    // 只要 i 或 j 是 UserPlayer，设置较大的 alpha 值
                    alphaParams[i].Add(0.5f);  // 假设较大值
                    betaParams[i].Add(0.01f);  // 假设 beta 统一为 0.01
                }
                else
                {
                    // 其他情况下，设置较小的 alpha 值
                    alphaParams[i].Add(0.01f); // 假设较小值
                    betaParams[i].Add(0.01f);  // 假设 beta 统一为 0.01
                }
            }
        }
    }

    private void Update()
    {
        PlayScore();
    }

    public void SetInitialPlayerTempo()
    {
        if (!initialTempoSet)
        {
            foreach (var player in players)
            {
                player.onsetInterval = originalAnimationDuration;
            }

            initialTempoSet = true;
        }
    }

    public bool NewOnsetsAvailable()
    {
        foreach (var player in players)
        {
            if (!player.notePlayed)
            {
                return false;
            }
        }
        return true;
    }

    public void CalculateNewIntervals()
    {
        foreach (var player in players)
        {
            player.RecalculateOnsetInterval(originalAnimationDuration, players, alphaParams[player.Index], betaParams[player.Index]);
        }
    }

    public void ClearOnsetsAvailable()
    {
        foreach (var player in players)
        {
            player.ResetNote();
            Debug.Log("Note reset");
        }
    }

    public void PlayScore()
    {
        // If all players have played a note, update timings.
        if (NewOnsetsAvailable())
        {
            CalculateNewIntervals();
            ClearOnsetsAvailable();
            ++scoreCounter;
            Debug.Log("scorecount" + scoreCounter);
        }
    }
}
