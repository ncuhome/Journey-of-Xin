using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���ڹ����������ʾ���л�
/// </summary>
public class SceneRoomManager : MonoBehaviour
{
    
    public void Change()//�л����³���
    {
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
