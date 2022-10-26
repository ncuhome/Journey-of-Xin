using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deform : MonoBehaviour
{
    private float timer = 2.5f;//持续时间计时器
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()//重启时重新计时
    {
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            Debug.Log("护盾激活中");
            timer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("护盾消失");
            gameObject.SetActive(false);
        }
    }
}
