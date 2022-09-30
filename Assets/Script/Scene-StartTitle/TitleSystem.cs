using TMPro;
using UnityEngine;

//���ڹ����������UI����
public class TitleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;//�����������
    private int cursor = 0;//��ǰ���λ��
   
    //0��Xin��������Ա������     1����ʼ�µ���Ϸ    2����Ϸ����      3��������Ϸ      4���˳�������Ϸ
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Up"))//ѡ������
        {
            animator.SetTrigger("up");
            cursor--;
            if (cursor < 0) { cursor = 4; }
           
        }
        else if(Input.GetButtonDown("Down"))//
        {
            animator.SetTrigger("down");
            cursor++;
            if(cursor > 4) { cursor = 0; }
        }
        else if(Input.GetButtonDown("Confirm"))//ȷ��
        {
            switch(cursor)
            {
                case 0://��ʾ����������
                    break;
                case 1://��ʼ�µ���Ϸ
                    break;
                case 2://��Ϸ����
                    break;
                case 3://�ع˾������������
                    break;
                case 4://�˳���Ϸ
                    Application.Quit();
                    break;
            }
        }
    }


}
