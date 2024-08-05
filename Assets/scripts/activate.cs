using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Activate : MonoBehaviour
{
    private Animator animator;
    public bool Switch = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 切换 Switch 的值
            Switch = !Switch;

            // 设置 Switch 参数
            animator.SetBool("Switch", Switch);

            // 记录当前时间到 TimeManager
            UserTimeManager.Instance.AddTimestamp(Time.time);

        }
    }
}
