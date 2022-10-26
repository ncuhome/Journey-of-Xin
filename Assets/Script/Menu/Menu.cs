using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void ReturnToGame()
    {
        MenuOptions.Instance.ReturnToGame();
    }
    public void ShowSettingsCanvas()
    {
        MenuOptions.Instance.ShowSettingsCanvas();
    }

    public void ShowSaveCanvas()
    {
        MenuOptions.Instance.ShowSaveCanvas();
    }
    public void ReturnToMainMenu()
    {
        MenuOptions.Instance.ReturnToMainMenu();
    }
    public void Exit()
    {
        MenuOptions.Instance.Exit();
    }
}
