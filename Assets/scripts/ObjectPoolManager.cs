using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    private List<GameObject> Playerpool;

    private void Awake()
    {
        // 确保只有一个实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保持对象池管理器实例
        }
        else
        {
            Destroy(gameObject); // 如果已经有一个实例，销毁新的实例
        }
    }

    void Start()
    {
        Playerpool = new List<GameObject>();

        // 查找场景中的所有 Metro 对象
        GameObject[] metroObjects = GameObject.FindGameObjectsWithTag("Metro");

        foreach (GameObject metro in metroObjects)
        {
            metro.SetActive(false);
            Playerpool.Add(metro);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in Playerpool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
