using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText; // 引用 Unity 的 Text 组件

    private int scoreCounter = 1;

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned!");
        }
        else
        {
            // 初始化文本内容
            UpdateScoreText(scoreCounter);
        }
    }

    public void UpdateScore(int newScore)
    {
        scoreCounter = newScore;
        UpdateScoreText(scoreCounter);
    }

    private void UpdateScoreText(int Counter)
    {
        if (scoreText != null)
        {
            scoreText.text = "Scorecount: " + Counter.ToString();
        }
    }
}
