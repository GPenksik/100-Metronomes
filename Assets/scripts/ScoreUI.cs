using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText; // 引用 Unity 的 Text 组件
    public Text BPMText;
    public Text AlphaText;

    private int scoreCounter = 0;

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
            UpdateBPMText(PlayerPrefs.GetFloat("BPM", 60f));
            UpdateAlphaText(PlayerPrefs.GetString("Alpha", "High"));
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
    public void UpdateBPMText(float bpm)
    {
        if (BPMText != null)
        {
            BPMText.text = "BPM: " + bpm.ToString();
        }
    }

    public void UpdateAlphaText(string name)
    {
        if (AlphaText != null)
        {
            AlphaText.text = "Alpha: " + name;
        }
    }
}
