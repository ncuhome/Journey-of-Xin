using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SecretNumberManager : MonoBehaviour
{
    public static SecretNumberManager Instance { get; private set; }
    public int[] numbersState = null;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        numbersState = new int[5];
    }

    void Start()
    {
        LoadSecretNumbers();
    }

    public void SaveSecretNumbers()
    {
        PlayerPrefs.SetInt("Number1",numbersState[0]);
        PlayerPrefs.SetInt("Number2",numbersState[1]);
        PlayerPrefs.SetInt("Number3",numbersState[2]);
        PlayerPrefs.SetInt("Number4",numbersState[3]);
        PlayerPrefs.SetInt("Number5",numbersState[4]);
    }

    public void LoadSecretNumbers()
    {
        numbersState[0] = PlayerPrefs.GetInt("Number1");
        numbersState[1] = PlayerPrefs.GetInt("Number2");
        numbersState[2] = PlayerPrefs.GetInt("Number3");
        numbersState[3] = PlayerPrefs.GetInt("Number4");
        numbersState[4] = PlayerPrefs.GetInt("Number5");
        Refresh();
    }

    public void Refresh()
    {
        if ((numbersState[0] == 1)&&(!StoreSystem.Instance.Find(16)))
        {
            StoreSystem.Instance.Add(16);
        }
        if ((numbersState[1] == 1)&&(!StoreSystem.Instance.Find(12)))
        {
            StoreSystem.Instance.Add(12);
        }
        if ((numbersState[2] == 1)&&(!StoreSystem.Instance.Find(26)))
        {
            StoreSystem.Instance.Add(26);
        }
        if ((numbersState[3] == 1)&&(!StoreSystem.Instance.Find(25)))
        {
            StoreSystem.Instance.Add(25);
        }
        if ((numbersState[4] == 1)&&(!StoreSystem.Instance.Find(27)))
        {
            StoreSystem.Instance.Add(27);
        }
    }

    #if UNITY_EDITOR
    [MenuItem("SecretNumbers/DeleteNumbers")]
    public static void DeleteNumbers()
    {
        PlayerPrefs.SetInt("Number1",0);
        PlayerPrefs.SetInt("Number2",0);
        PlayerPrefs.SetInt("Number3",0);
        PlayerPrefs.SetInt("Number4",0);
        PlayerPrefs.SetInt("Number5",0);
    }
    #endif
}
