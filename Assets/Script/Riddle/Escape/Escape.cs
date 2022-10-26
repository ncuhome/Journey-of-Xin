using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject Star;
    private float starCDTimer = 0;//生成星星的计时器
    public void End()//失败
    {

    }
    public void EndSuccess()//成功
    {

    }

    private void CreatStar()//生成星星
    {
        GameObject star = Instantiate(Star, new Vector3(2000, 10 + Random.value * 1000, 0), Quaternion.identity);
        StarMove starMove = star.GetComponent<StarMove>();
        starMove.dirction = new Vector3(-100, -30 + Random.value * 30, 0) * 10 * (1+Random.value*2);//随机方向
        float size = 40 + 100 * Random.value;
        star.transform.localScale = new Vector3(size,size,0);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(starCDTimer <= 0)//冷却完成
        {
            CreatStar();
            CreatStar();
            CreatStar();
            if (Random.value > 0.5)
            {
                CreatStar();
                CreatStar();
            }
            starCDTimer = 0.5f+Random.value;
        }
        else
        {
            starCDTimer -= Time.deltaTime;
        }
    }
}
