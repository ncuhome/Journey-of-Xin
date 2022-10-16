using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 用于管理场景间的显示与切换
/// </summary>
public class SceneRoomManager : MonoBehaviour
{
    
    public void Change()//切换至新场景
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
