using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    public bool IsReversed = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator component not found on" + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimationSpeed(float speed)
    {
        if (animator != null)
        {
            animator.speed = speed;
            Debug.Log("Animation s00000peed set to: " + speed);
        }
    }

    public float GetAnimationSpeedSign()
    {
        //return animator != null ? animator.speed : 0f;
        if (animator != null)
        {
            return animator.speed >= 0 ? 1 : -1;
        }
        else
        {
            return 0; // Animator 组件不存在时返回0，表示没有速度
        }
    }

    public void SetAnimationSpeedsign()
    {
        IsReversed = !IsReversed;
        animator.SetBool("IsReversed", IsReversed);    
    }
}
