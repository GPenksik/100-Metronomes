using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 如果使用TextMeshPro，改为 using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Text meanOnsetText; // 如果使用TextMeshPro，改为 public TextMeshProUGUI meanOnsetText;
    public Text meanIntervalText; // 同上
    public Text onsetIntervalText; // 同上

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // 恢复游戏时间
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // 暂停游戏时间
        isPaused = true;

        // 在暂停菜单激活时，更新显示的参数
        UpdateOnsetValues();
    }

    private void UpdateOnsetValues()
    {
        float meanOnset = PlayerPrefs.GetFloat("meanOnset", 0f);
        float meanInterval = PlayerPrefs.GetFloat("meanInterval", 0f);
        float onsetInterval = PlayerPrefs.GetFloat("onsetInterval", 0f);

        meanOnsetText.text = "Mean Onset: " + meanOnset.ToString("F2");
        meanIntervalText.text = "Mean Interval: " + meanInterval.ToString("F2");
        //onsetIntervalText.text = "Onset Interval: " + onsetInterval.ToString("F2");
    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1f; // 恢复游戏时间
        UserTimeManager.Instance.clearTimestamps();
        SceneManager.LoadScene("StartMenu"); // 替换为你的启动菜单场景名
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
