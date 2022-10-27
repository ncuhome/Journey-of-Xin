
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//����6
public class InterstellarPassage : MonoBehaviour
{
    // Start is called before the first frame update
    #region ��ͼ�ƶ�
    public GameObject Map0,Map1,Map2;//������ͼ
    private float gameTimer = 0;//��Ϸ��ʱ��
    private float cdTimer = 0;//cd��ʱ��
    private float creatCD = 0,creatWaitTime = 5.1f;//ʯͷ������ȴʱ��
    private float speedStone = 5;
    public static float speedMap = 5;//��ͼ�ƶ��ٶ�
    public Camera Camera;//�����
    public GameObject StoneS, StoneM, StoneL;//����ʯͷ

    public GameObject tipsButton;
    public void TipsDisplay()
    {
        if (!tipsButton.activeSelf)
        {
            tipsButton.SetActive(true);
        }
        else
        {
            tipsButton.SetActive(false);
        }
    }
    private void MapMove()//��ͼ����
    {
        Map0.transform.localPosition -= new Vector3(speedMap, 0, 0);
        Map1.transform.localPosition -= new Vector3(speedMap, 0, 0);
        Map2.transform.localPosition -= new Vector3(speedMap, 0, 0);
        if (Map0.transform.localPosition.x <= -950)
        {
            Map0.transform.localPosition = new Vector3(2850, 540, 200);
        }
        if (Map1.transform.localPosition.x <= -950)
        {
            Map1.transform.localPosition = new Vector3(2850, 540, 200);
        }
        if (Map2.transform.localPosition.x <= -950)
        {
            Map2.transform.localPosition = new Vector3(2850, 540, 200);
        }
    }
    public void CanmeraRock()//�������
    {
        Camera.gameObject.GetComponent<Animator>().SetTrigger("rock");
    }
    public void End()//����
    {

        EventSystem.Instance.ActiveEvent(108);
        InputManager.Instance.sceneState = SceneState.MainScene;
        SceneManager.UnloadSceneAsync(13);
    }
    #endregion


    private void StoneCreat()
    {
        double number = Random.value;
        if (number < 0.7)
        {
            Instantiate(StoneS,new Vector3(3000 + Random.value*1200,(float)(900* Random.value), 0), Quaternion.identity)
                .GetComponent<StoneMove>().dirction = (new Vector3(-100, -15 + Random.value*30,0)).normalized * (2+speedStone*Random.value*1.5f);
            StoneCreat();
        }
        else if(number < 0.9)
        {
            Instantiate(StoneM, new Vector3(3000 + Random.value * 1200, (float)(900 * Random.value), 0), Quaternion.identity)
                .GetComponent<StoneMove>().dirction = (new Vector3(-100, -15 + Random.value * 30, 0)).normalized * (2+speedStone*Random.value *1.5f);
        }
        else
        {
            Instantiate(StoneL, new Vector3(3000 + Random.value * 1200, (float)(900 * Random.value), 0), Quaternion.identity)
                .GetComponent<StoneMove>().dirction = (new Vector3(-100, -15 + Random.value * 30, 0)).normalized * (2+speedStone*Random.value *1.5f);
        }
    }

    private bool closeTips = true;
    void Update()
    {
        gameTimer += Time.deltaTime;
        if(closeTips)
        {
            if(gameTimer > 2) 
            {
                TipsDisplay();
                closeTips = false;
            }
        }
        if(speedStone < 15){ speedStone = 5 + gameTimer / 5; }
        if (speedMap < 25) { speedMap = 5 + gameTimer / 2; }
        if(creatWaitTime > 0.25) { creatWaitTime -= Time.deltaTime/10; }
        Debug.Log("CDTime:" + creatWaitTime);
        MapMove(); 
        if(cdTimer <= 0)
        {
            StoneCreat();
            if (Random.value*10 > 6) 
            {   
                StoneCreat();
            }
            cdTimer = creatWaitTime*(0.5f+Random.value*0.5f);
        }
        else
        {
            cdTimer-=Time.deltaTime;
        }
    }
}
