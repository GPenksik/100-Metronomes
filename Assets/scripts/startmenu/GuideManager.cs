using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public static GuideManager Instance { get; private set; }

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

    public void DestroyOnSceneLoad()
    {
        Destroy(gameObject);
    }
}