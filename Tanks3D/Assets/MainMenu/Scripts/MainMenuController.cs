using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button StartGame, Exit;

    private void Awake()
    {
        StartGame.onClick.AddListener(StartGameClick);
        Exit.onClick.AddListener(ExitClick);
    }

    private void StartGameClick()//first level
    {
        SceneManager.LoadScene(1);
    }
    private void ExitClick()
    {
        Application.Quit();
    }

}
