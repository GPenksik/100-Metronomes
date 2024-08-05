using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsembleStars : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject();
            if (obj != null)
            {
                // 对对象进行一些操作
                Debug.Log("Object retrieved from pool and activated.");
            }
            else
            {
                Debug.Log("No available objects in the pool.");
            }
        }
    }
}
