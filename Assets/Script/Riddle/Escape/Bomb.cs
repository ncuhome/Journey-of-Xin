using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(10*Time.deltaTime,10*Time.deltaTime,0);//»¤¶ÜÅòÕÍ
        if(transform.localScale.magnitude > 30)
        {
            gameObject.SetActive(false);
        }
    }
}
