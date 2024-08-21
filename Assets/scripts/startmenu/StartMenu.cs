using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1f; // 恢复游戏时间
        UserTimeManager.Instance.clearTimestamps();
        GuideManager.Instance.ReloadChildObject();
    }
    void Update()
    {
        if (UserTimeManager.Instance.GetFiveTimestamps())
        {
            // 记录当前时间为 starttime（使用 Time.time）
            float StartTime = Time.time;
            //Debug.Log("StartTime" + StartTime);
            PlayerPrefs.SetFloat("StartTime", StartTime);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Ensemble");
        }
    }

}
