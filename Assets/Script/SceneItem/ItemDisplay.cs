using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 用于播放场景中可收集物品的消失动画
/// </summary>
public class ItemDisplay : MonoBehaviour
{
    private float a = 1.0f;
    private float timer = 0;
    private int status = 0;
    /// <summary>
    /// 调用此函数时，播放动画并在结束后销毁物体
    /// </summary>
    public void DisplayStart()
    {
        status = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch(status)
        {
            case 1://将物体平移至中心
                gameObject.transform.localPosition -= gameObject.transform.localPosition * Time.deltaTime * 5;
                if (gameObject.transform.localPosition.magnitude <= 10)
                {
                    GetComponent<Animator>().SetBool("Click",false);
                    gameObject.transform.localPosition = Vector3.zero;
                    status = 2;
                }
                break;
            case 2:
                timer += Time.deltaTime;
                //gameObject.transform.localScale += Vector3.one * Time.deltaTime * 5;
                //gameObject.GetComponent<RectTransform>().localScale += Vector3.one * Time.deltaTime * 5;
                a -= Time.deltaTime * (1/(1+timer));
                gameObject.GetComponent<Image>().color = new Color(255,255,255,a);
                if (a <= 0.5f) { status = 3; }
                break;
            case 3:
                Destroy(gameObject);
                break;
        }
    }
}
