using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Playeranimation : MonoBehaviour
{
    private Animator dialAnimator;
    private bool notePlayed;
    private List<float> animationEndTimes = new List<float>();

    void Start()
    {
        notePlayed = false;
        dialAnimator = GetComponent<Animator>();
        if (dialAnimator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimationEnd();
    }

    public void AnimationEnd()
    {
        AnimatorStateInfo stateInfo = dialAnimator.GetCurrentAnimatorStateInfo(0);

        // 如果动画已经播放完毕，normalizedTime >= 1 表示动画已播放完
        if (stateInfo.normalizedTime >= 1 && !dialAnimator.IsInTransition(0))
        {
            //PlayerTimemanager.Instance.AddAnimationEndTime(Time.time);
            PlayNote();
        }

    }

    public bool HasNotePlayed()
    {
        return notePlayed;
    }

    public void PlayNote()
    {
        notePlayed = true;
        dialAnimator.SetBool("notePlayed", notePlayed);
    }

    public void ResetNote()
    {
        notePlayed = false;
    }

    public float getLatestOnsetTime()
    {
        if (animationEndTimes.Count > 0)
        {
            return animationEndTimes[animationEndTimes.Count - 1];
        }
        else
        {
            Debug.LogWarning("No animation end times recorded yet");
            return -1f;
        }
    }

    public List<float> getOnsetTimes()
    {
        return new List<float>(animationEndTimes);
    }
}


