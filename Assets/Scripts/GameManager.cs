using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour {

    public static GameManager instance;

    public GameObject canvas;
    public GameObject scoreUI;

    [Header("Clock")]
    public GameObject clockUI;
    public int countdown;
    public int gameTimer;
    public TextMeshProUGUI middleSceneText;
    public Text timerText;

    [Header("Scoreboard")]
    public GameObject scoreboard;
    public TMPro.TextMeshProUGUI[] playersNameInScoreboard;

    [Header("Sound at Game Start")]
    public AudioSource audioPlayer;
    public AudioClip startSound;
    public AudioClip timeUpSound;
    public AudioClip tadaSound;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        canvas.SetActive(true);
        scoreUI.SetActive(true);
        scoreboard.SetActive(false);
        clockUI.SetActive(false);
        StartGame(gameTimer);
        Invoke("PlayStartSound", countdown-1);
        //InvokeRepeating("Counting", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown <= 3 && countdown > 0)
        {
            middleSceneText.text = countdown.ToString();
        }
        else if (countdown <= 0 && countdown > -3)
        {
            middleSceneText.text = "Start!!";
        }
        else if (gameTimer == 0)
        {
            middleSceneText.text = "Game End!!";
            CancelInvoke("Counting");
            Invoke("ShowScoreboard", 10);
        }
        else
        {
            middleSceneText.text = "";
        }
        int minute = gameTimer / 60;
        int second = gameTimer % 60;
        timerText.text = string.Format("{0:D2} : {1:D2}", minute, second);
	}

    public void Counting()
    {
        if (countdown > -3)
        {
            countdown--;
        }
        if (countdown < 0)
        {
            gameTimer--;
        }
    }

    public void StartGame(int t)
    {
        gameTimer = t;
        clockUI.SetActive(true);
        InvokeRepeating("Counting", 0f, 1f);
    }

    public void ShowScoreboard()
    {
        if (scoreboard.activeInHierarchy)
        {
            return;
        }
        clockUI.SetActive(false);
        scoreUI.SetActive(false);
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        int pAmount = p.Length;
        GamePlayerInfo[] d = new GamePlayerInfo[p.Length];
        for (int i = 0; i < pAmount; i++)
        {
            int highest = 0;
            int scoreHighest = -9999;
            for (int j = 0; j < p.Length; j++) {
                if (p[j] != null && p[j].GetComponent<GamePlayerInfo>().score > scoreHighest)
                {
                    highest = j;
                    scoreHighest = p[j].GetComponent<GamePlayerInfo>().score;
                }
            }
            d[i] = p[highest].GetComponent<GamePlayerInfo>();
            p[highest] = null;
        }
        int k = 0;
        foreach (GamePlayerInfo player in d)
        {
            playersNameInScoreboard[k].text = player.playerName;
            k++;
        }
        scoreboard.SetActive(true);
        PlayTadaSound();
        Invoke("BackToMenu", 10);
    }

    public void BackToMenu()
    {
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopServer();
        SceneManager.LoadScene(0);
    }

    public void PlayStartSound()
    {
        audioPlayer.clip = startSound;
        audioPlayer.Play();
        Invoke("PlayTimeUpSound", gameTimer);
    }

    public void PlayTadaSound()
    {
        audioPlayer.clip = tadaSound;
        audioPlayer.Play();
    }

    public void PlayTimeUpSound()
    {
        audioPlayer.clip = timeUpSound;
        audioPlayer.volume = 0.3f;
        audioPlayer.Play();
    }
}
