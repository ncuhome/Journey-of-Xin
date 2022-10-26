using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    public Vector3 dirction;//移动方向

    void Update()
    {
        transform.position += dirction*Time.deltaTime;
        if(transform.position.x < -1400 || transform.position.y > 1200 || transform.position.y < -1200)
        {
            Destroy(gameObject);
        }
    }
}
