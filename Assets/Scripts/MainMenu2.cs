using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class MainMenu2: MonoBehaviour
{
    public static MainMenu instance;

    public GameObject mainMenu;
    public GameObject settingPanel;

    private void Start()
    {
        ToMenuScreen();
    }

    public void OpenSettingScreen()
    {
        mainMenu.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void ToMenuScreen()
    {
        settingPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToPlayScreen()
    {
        if (GameObject.Find("LobbyManager") != null)
        {
            Destroy(GameObject.Find("LobbyManager"));
        }
        SceneManager.LoadScene("ExampleMatchmaking");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
