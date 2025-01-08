using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public static GuideManager Instance { get; private set; }

    public GameObject visualPrefab;  // 用于存储要加载的 Prefab
    public GameObject emptyObject;  // 用于存储要加载的空对象

    public int needvisual;  // 控制是否加载 visualPrefab
    public int needaudio;  // 控制是否加载 emptyObjectPrefab


    void Awake()
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

    void Start()
    {
        ReloadChildObject();
    }


    void LoadChildObject(int visual)
    {
        if (visualPrefab != null)
        {
            visualPrefab.SetActive(false);
        }
        if (emptyObject != null)
        {
            emptyObject.SetActive(false);
        }

        // 根据 visual 和 audio 的值激活对应的对象
        if (visual == 1 && visualPrefab != null)
        {
            visualPrefab.SetActive(true);
        }
        else if (visual == 0 && emptyObject != null)
        {
            emptyObject.SetActive(true);
        }
    }

    // 在场景加载时调用，销毁该对象
    public void DestroyOnSceneLoad()
    {
        Destroy(gameObject);
    }

    // 你可以在 Inspector 中通过调用此函数手动重新加载对象
    public void ReloadChildObject()
    {
        needvisual = PlayerPrefs.GetInt("VisualGudance", 1);
        LoadChildObject(needvisual);
    }
}
