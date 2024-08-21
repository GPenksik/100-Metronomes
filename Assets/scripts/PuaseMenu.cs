using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseAndConfirmMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 引用整个暂停/确认菜单的 UI
    public Button confirmButton; // 引用确认按钮
    public Text Timeover;
    private bool isPaused = false;

    void Start()
    {
        // 绑定确认按钮的点击事件
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        // 默认隐藏 UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // 默认隐藏 UI
        if (Timeover != null)
        {
            Timeover.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        if(Time.time - PlayerPrefs.GetFloat("StartTime") > 120f)
        {
            Pause();
            Timeover.enabled = true;
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
    }

    public void LoadSettingsMenu()
    {
        //UserTimeManager.Instance.clearTimestamps();
        SceneManager.LoadScene("Settings"); // 替换为你的启动菜单场景名
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnConfirmButtonClicked()
    {
        SaveDataToFile();
        LoadSettingsMenu(); // 确认后切换到 Start Menu 场景
    }

    void SaveDataToFile()
    {
        try
        {
            // 获取保存文件夹路径
            string directoryPath = Application.dataPath + "/SavedData";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = "PlayerData_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            string filePath = Path.Combine(directoryPath, fileName);

            StringBuilder data = new StringBuilder();

            // 保存列名（例如，"Key,Value"）
            data.AppendLine("Key,Value");

            // 保存 PlayerPrefs 数据，包括 starttime
            foreach (var key in PlayerPrefsKeys())
            {
                if (PlayerPrefs.HasKey(key))
                {
                    if (key == "BPM" || key == "alphaUser" || key == "alphaAuto" || key == "StartTime")
                    {
                        float value = PlayerPrefs.GetFloat(key);
                        data.AppendLine($"{key},{value.ToString("F4")}");
                    }
                    else
                    {
                        string value = PlayerPrefs.GetString(key, "N/A");
                        data.AppendLine($"{key},{value}");
                    }
                }
                else
                {
                    data.AppendLine($"{key},N/A");
                }
            }

            UserPlayer userPlayer = FindObjectOfType<UserPlayer>();
            if (userPlayer != null)
            {
                List<float> meanOnsetList = userPlayer.GetMeanOnsetList();
                List<float> meanIntervalList = userPlayer.GetMeanIntervalList();

                // 保存 meanOnsetList 在同一行
                data.Append("Mean Onset List,");
                if (meanOnsetList.Count > 0)
                {
                    for (int i = 0; i < meanOnsetList.Count; i++)
                    {
                        data.Append($"{meanOnsetList[i].ToString("F4")}");
                        if (i < meanOnsetList.Count - 1)
                        {
                            data.Append(","); // 在每个值之间加逗号
                        }
                    }
                }
                else
                {
                    data.Append("No mean onset values recorded.");
                }
                data.AppendLine(); // 在最后添加一个换行符

                // 保存 meanIntervalList 在同一行
                data.Append("Mean Interval List,");
                if (meanIntervalList.Count > 0)
                {
                    for (int i = 0; i < meanIntervalList.Count; i++)
                    {
                        data.Append($"{meanIntervalList[i].ToString("F4")}");
                        if (i < meanIntervalList.Count - 1)
                        {
                            data.Append(","); // 在每个值之间加逗号
                        }
                    }
                }
                else
                {
                    data.Append("No mean interval values recorded.");
                }
                data.AppendLine(); // 在最后添加一个换行符
            }

            // 获取并保存 UserTimeManager 中的 Timestamps 数据
            List<float> timestamps = UserTimeManager.Instance.GetClapTimestamps();

            // 保存 Timestamps 数据在同一行
            data.Append("Timestamps,");
            if (timestamps.Count > 0)
            {
                for (int i = 0; i < timestamps.Count; i++)
                {
                    data.Append($"{timestamps[i].ToString("F4")}");
                    if (i < timestamps.Count - 1)
                    {
                        data.Append(","); // 在每个值之间加逗号
                    }
                }
            }
            else
            {
                data.Append("No timestamps recorded.");
            }
            data.AppendLine(); // 在最后添加一个换行符

            // 将数据写入新文件
            File.WriteAllText(filePath, data.ToString());

            Debug.Log("Data saved to " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save data: " + e.Message);
        }
    }

    string[] PlayerPrefsKeys()
    {
        return new string[] { "BPM", "StartTime", "Alpha","alphaUser", "alphaAuto" };
    }

}
