using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ClapControlledMetronome : MonoBehaviour
{
    public Transform dialPivotCenter;
    public float rotationSpeed = 90f; // Rotation speed (degrees per second)
    public float maxRotationAngle = 60f; // Maximum rotation angle
    public float minTimeBetweenFlips = 0.1f; // Minimum flip interval (seconds)
    public float maxTimeBetweenFlips = 5f;
    public float LastClapTime { get { return lastClapTime; } }
    //public float TimeBetweenClaps { get { return timeBetweenClaps; } }

    private float lastFlipTime;
    private float lastClapTime;
    private bool canDetectClap = true;
    private const string microphoneDevice = null; // Specify the name of the microphone device, use null for the default microphone
    private const int sampleRate = 20000;


    // Add an AudioSource variable for playing sound effects
    public AudioSource audioSource;
    public AudioClip soundEffect;
    public float amplitudeThreshold = 0.5f; // Threshold for detecting clap actions

    bool isMetronomeStarted = false;

    private Coroutine rotatorRoutine;


    public class Clapdetection : MonoBehaviour
{

    public int sampleRate = 44100;
    public int bufferLength = 2048;
    public float volumeThreshold = 0.8f;
    private AudioClip microphoneInput;
    private bool microphoneInitialized = false;
    private float[] audioBuffer;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMicrophone();
    }

    void InitializeMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 10, sampleRate);
            microphoneInitialized = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (microphoneInitialized)
        {
            ProcessAudio();
        }

    }
    void ProcessAudio()
    {
        audioBuffer = new float[bufferLength];
        int microphonePosition = Microphone.GetPosition(null) - (bufferLength + 1); // 获取当前麦克风位置
        if (microphonePosition < 0)
            return;

        microphoneInput.GetData(audioBuffer, microphonePosition);

        // 检测音量是否超过阈值
        float levelMax = 0;
        for (int i = 0; i < bufferLength; i++)
        {
            float wavePeak = audioBuffer[i] * audioBuffer[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        if (levelMax > volumeThreshold)
        {
            Debug.Log("Clap detected");
            // 处理拍手声后的逻辑
        }
    }

}


    void Start()
    {
        lastFlipTime = Time.time;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start recording microphone audio
        StartMicrophone();
    }

    void Update()
    {

    }

    bool DetectClap()
    {
        if (!canDetectClap)
        {
            return false;
        }

        float[] spectrum = new float[256];
        float amplitude = 0f;

        // Get microphone audio data
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        for (int i = 0; i < spectrum.Length; i++)
        {
            amplitude += Mathf.Abs(spectrum[i]);
        }
        amplitude /= spectrum.Length;

        if (amplitude > amplitudeThreshold)
        {
            // Record the time interval between two claps
            //timeBetweenClaps = Time.time - lastClapTime;

            // Update the last clap time
            lastClapTime = Time.time;

            // Disable clap detection to prevent multiple detections
            canDetectClap = false;
            Debug.Log("amplitude:" + amplitude);

            // Enable clap detection again after a delay of 0.5 seconds
            StartCoroutine(EnableClapDetectionAfterDelay(0.5f));

            // Return true indicating a detected clap
            return true;
        }
        // Return false indicating no clap detected
        return false;
    }

    private void StartMicrophone()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone detected!");
            return;
        }

        string selectedMicrophone = string.IsNullOrEmpty(microphoneDevice) ? Microphone.devices[0] : microphoneDevice;

        audioSource.clip = Microphone.Start(selectedMicrophone, true, 10, sampleRate);
        audioSource.loop = true;

        // Wait for recording to start
        while (!(Microphone.GetPosition(selectedMicrophone) > 0)) { }

        // Play the recorded audio
        audioSource.Play();
    }

    void OnDisable()
    {
        // Stop microphone recording
        Microphone.End(null);
    }

    IEnumerator EnableClapDetectionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canDetectClap = true;  // Enable clap detection
    }


}