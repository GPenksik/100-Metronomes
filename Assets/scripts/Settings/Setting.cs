using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Button button60;
    public Button button90;
    public Button button120;
    public Button buttonHigh;
    public Button buttonMedium;
    public Button buttonLow;
    public Button nextButton;

    private Button selectedBpmButton;
    private Button selectedAlphaButton;
    private float bpm = 120f;

    void Start()
    {
        // 设置每个按钮的点击事件
        button60.onClick.AddListener(() => OnBpmButtonClicked(button60, 60f));
        button90.onClick.AddListener(() => OnBpmButtonClicked(button90, 90f));
        button120.onClick.AddListener(() => OnBpmButtonClicked(button120, 120f));
        buttonHigh.onClick.AddListener(() => OnAlphaButtonClicked(buttonHigh, 0.1f, 0.03f));
        buttonMedium.onClick.AddListener(() => OnAlphaButtonClicked(buttonMedium, 0.03f, 0.03f));
        buttonLow.onClick.AddListener(() => OnAlphaButtonClicked(buttonLow, 0.01f, 0.03f));

        nextButton.onClick.AddListener(OnNextButtonClicked);

        // 默认选择 120 BPM 和 Medium Alpha 的按钮
        OnBpmButtonClicked(button90, 90f);
        OnAlphaButtonClicked(buttonMedium, 0.03f, 0.03f);
    }

    void OnBpmButtonClicked(Button button, float selectedBpm)
    {
        bpm = selectedBpm; // 更新 BPM 值

        // 将 BPM 值存储在 PlayerPrefs 中，实时更新
        PlayerPrefs.SetFloat("BPM", bpm);
        PlayerPrefs.Save();

        // 更新按钮外观
        if (selectedBpmButton != null)
        {
            ResetButtonAppearance(selectedBpmButton);
        }

        selectedBpmButton = button;
        SetButtonSelectedAppearance(button);
    }

    void OnAlphaButtonClicked(Button button, float alphaUser, float alphaAuto)
    {
        // 将 Alpha 值存储在 PlayerPrefs 中，实时更新
        PlayerPrefs.SetFloat("alphaUser", alphaUser);
        PlayerPrefs.SetFloat("alphaAuto", alphaAuto);
        PlayerPrefs.Save();

        // 更新按钮外观
        if (selectedAlphaButton != null)
        {
            ResetButtonAppearance(selectedAlphaButton);
        }

        selectedAlphaButton = button;
        SetButtonSelectedAppearance(button);
    }

    void ResetButtonAppearance(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        buttonText.color = Color.black; // 重置文字颜色
        button.GetComponent<Image>().color = Color.white; // 重置背景颜色
    }

    void SetButtonSelectedAppearance(Button button)
    {
        Text buttonText = button.GetComponentInChildren<Text>();
        buttonText.color = Color.white; // 选中文字颜色
        button.GetComponent<Image>().color = Color.gray; // 选中背景颜色
    }

    void OnNextButtonClicked()
    {
        // 加载 StartMenu 场景
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
