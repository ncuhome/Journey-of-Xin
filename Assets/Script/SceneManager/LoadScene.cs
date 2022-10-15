using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    private void Awake()
    {
        LoadMainScene();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
        SceneManager.LoadScene(7, LoadSceneMode.Additive);
    }
}
