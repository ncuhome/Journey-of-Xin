using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    public string passwordString = null;
    public int[] password = null;
    public TMP_Text passwordText = null;
    public int passwordIndex = 0;
    private string correctPassword = "12345";
    private DialogueTrigger correctTrigger = null;
    private DialogueTrigger wrongTrigger = null;
    private GameObject letterBox = null;

    void Awake()
    {
        passwordText = GameObject.Find("Password").GetComponent<TMP_Text>();
        correctTrigger = transform.Find("CorrectDialog").GetComponent<DialogueTrigger>();
        wrongTrigger = transform.Find("WrongDialog").GetComponent<DialogueTrigger>();
        letterBox = GameObject.Find("LetterBox");
    }
    // Start is called before the first frame update
    void Start()
    {
        passwordString = "-----";
        password = new int[5];
        passwordIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePassword()
    {
        passwordString = "";
        for (int i = 0; i < 5; i++)
        {
            if (i < passwordIndex)
            {
                passwordString += password[i].ToString();
            }
            else
            {
                passwordString += "-";
            }
        }
        passwordText.text = passwordString;
    }

    public void GetButton0()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 0;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton1()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 1;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton2()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 2;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton3()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 3;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton4()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 4;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton5()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 5;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton6()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 6;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton7()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 7;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton8()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 8;
        passwordIndex++;
        UpdatePassword();
    }
    public void GetButton9()
    {
        if (passwordIndex == 5)
        {
            return;
        }
        password[passwordIndex] = 9;
        passwordIndex++;
        UpdatePassword();
    }
    public void Delete()
    {
        if (passwordIndex == 0)
        {
            return;
        }
        passwordIndex--;
        UpdatePassword();
    }
    public void Confirm()
    {
        if (passwordString == correctPassword)
        {
            Debug.Log("正确");
            correctTrigger.StartDialogue();
            letterBox.GetComponent<Button>().interactable = false;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("错误");
            passwordIndex = 0;
            UpdatePassword();
            wrongTrigger.StartDialogue();
            gameObject.SetActive(false);
        }
    }

}
