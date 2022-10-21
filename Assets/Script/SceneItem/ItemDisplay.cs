using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���ڲ��ų����п��ռ���Ʒ����ʧ����
/// </summary>
public class ItemDisplay : MonoBehaviour
{
    private float a = 1.0f;
    private float timer = 0;
    private int status = 0;
    private Vector3 target = Vector3.zero;
    public GameObject panel = null;
    /// <summary>
    /// ���ô˺���ʱ�����Ŷ������ڽ�������������
    /// </summary>
    public void DisplayStart()
    {
        status = 1;
        GetComponent<Animator>().SetBool("Click", false);
        panel.SetActive(false);
    }

    public void Click()
    {
        if (panel.activeInHierarchy) { return; }
        Debug.Log("TestNodw");
        target = new Vector3(-850, -440, 0);
        gameObject.transform.localPosition = Vector3.zero;
        GetComponent<Animator>().SetBool("Click", true);
        GetComponent<DialogueTrigger>().StartDialogue();
        panel.SetActive(true);
        transform.SetSiblingIndex(transform.parent.childCount);
    }

    void Awake()
    {
       // panel = GameObject.Find("Canvas/Panel");
    }

    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 1://������ƽ��������
                gameObject.transform.localPosition += (target - gameObject.transform.localPosition) * Time.deltaTime * 3;
                if ((target - gameObject.transform.localPosition).magnitude <= 10)
                {
                    gameObject.transform.localPosition = target;
                }
                timer += Time.deltaTime;
                a -= Time.deltaTime * (1 / (1 + timer));
                gameObject.GetComponent<Image>().color = new Color(255, 255, 255, a);
                if (a <= 0.2f) { status = 2; }
                break;
            case 2:
                Destroy(gameObject);
                break;
        }
    }
}
