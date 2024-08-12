using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button button60;
    public Button button90;
    public Button button120;
    public Button startButton;

    private float bpm = 120f; // 默认 BPM 为120

    void Start()
    {
        // 设置每个按钮的点击事件
        button60.onClick.AddListener(() => OnBpmButtonClicked(button60, 60f));
        button90.onClick.AddListener(() => OnBpmButtonClicked(button90, 90f));
        button120.onClick.AddListener(() => OnBpmButtonClicked(button120, 120f));

        startButton.onClick.AddListener(OnStartButtonClicked);

        // 默认选择 120 BPM 的按钮
        OnBpmButtonClicked(button120, 120f);
    }

    void OnBpmButtonClicked(Button button, float selectedBpm)
    {
        bpm = selectedBpm; // 更新 BPM 值

        // 将 BPM 值存储在 PlayerPrefs 中，实时更新
        PlayerPrefs.SetFloat("BPM", bpm);
        PlayerPrefs.Save();

    }

    void OnStartButtonClicked()
    {
        // 加载 Ensemble 场景
        SceneManager.LoadScene("Ensemble");
    }
}
