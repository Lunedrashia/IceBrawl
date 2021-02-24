using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public GameObject mainMenu;
    public GameObject playMenu;

    public GameObject clientPrefab;
    public GameObject serverPrefab;

    public InputField nameInput;

    private void Start()
    {
        ToMenuScreen();
    }

    public void ToPlayScreen()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void ToMenuScreen()
    {
        playMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ConnectToServerButton()
    {
        string hostAddress = GameObject.Find("IP Input").GetComponent<InputField>().text;
        if (hostAddress == null)
        {
            hostAddress = "127.0.0.1";
        }

        try
        {
            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.clientName = nameInput.text;
            if (c.clientName == "")
            {
                c.clientName = "Anonymous";
            }
            c.ConnectToServer(hostAddress, 7070);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Destroy(FindObjectOfType<Client>());
        }
    }

    public void HostButton()
    {
        try
        {
            Server s = Instantiate(serverPrefab).GetComponent<Server>();
            s.Init();
            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.clientName = nameInput.text;
            if (c.clientName == "")
            {
                c.clientName = "Host";
            }
            c.ConnectToServer("127.0.0.1", 7070);
            SceneManager.LoadScene("Lobby");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Destroy(FindObjectOfType<Server>());
            Destroy(FindObjectOfType<Client>());
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}