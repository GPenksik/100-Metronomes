using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioOnly : MonoBehaviour
{
    public AudioClip audioClips;
    private AudioSource audioSource;
    private float defaultBpm = 120f;
    private float bpm;
    private bool isInvokingPlayAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClips;
        audioSource.pitch = 2f;
        float WaitTime = Updateinterval();

        InvokeRepeating("PlayAudioClip", 0f, WaitTime);
        isInvokingPlayAudio = true;
    }

    // Update is called once per frame
    void Update()
    {
        int UseAudio = PlayerPrefs.GetInt("AudioGudance", 0);
        int Usevisual = PlayerPrefs.GetInt("VisualGudance", 0);
        if (Usevisual == 0 )
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        if (Usevisual == 1 && isInvokingPlayAudio) 
        {
            CancelInvoke("PlayAudioClip");
            isInvokingPlayAudio = false;
        }
        if (UseAudio == 0 && Usevisual == 0)
        {
            SceneManager.sceneLoaded += OnSceneLoadednone;
        }

    }
    void PlayAudioClip()
    {
        audioSource.Play();
    }
    public float Updateinterval()
    {
        bpm = PlayerPrefs.GetFloat("BPM", defaultBpm);
        float Speed =  defaultBpm / bpm;
        return Speed * 0.5f;
    }
    void OnSceneLoadednone(Scene scene, LoadSceneMode mode)
    {
        // 检查场景名称是否为 "Ensemble"
        if (scene.name == "Ensemble")
        {
            // 停止重复调用 PlayAudioClip
            CancelInvoke("PlayAudioClip");
            isInvokingPlayAudio = false;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查场景名称是否为 "Ensemble"
        if (scene.name == "StartMenu")
        {
            float WaitTime = Updateinterval();
            InvokeRepeating("PlayAudioClip", 0f, WaitTime);
            Debug.Log("here");
            isInvokingPlayAudio = true;
        }
    }
}
