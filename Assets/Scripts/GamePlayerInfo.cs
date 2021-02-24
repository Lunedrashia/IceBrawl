using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GamePlayerInfo : NetworkBehaviour {

    private NetworkStartPosition[] spawnPoints;
    public Rigidbody thisRG;
    public GameObject trail;
    [SyncVar] public string playerName;
    public GamePlayerInfo lastHit;
    [SyncVar] public int score = 0;

    public Text scoreText;
    public GameObject[] mesh;

    [SyncVar] private bool isSpawning = false;
    private float t = 3;
    private bool displaying = true;

    [ClientCallback]
    private void Start()
    {
        if(isLocalPlayer)
        {
            spawnPoints = GameObject.FindObjectsOfType<NetworkStartPosition>();
            score = 0;
            scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        }
    }

    [ClientCallback]
    private void Update()
    {
        if (scoreText == null)
        {
            return;
        }

        if (!isLocalPlayer)
        {
            scoreText.gameObject.SetActive(false);
            return;
        }
        scoreText.text = score.ToString();
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        if (isLocalPlayer && !isSpawning)
        {
            thisRG.constraints = RigidbodyConstraints.FreezeAll;
            trail.GetComponent<TrailRenderer>().time = 0;
            isSpawning = true;
            InvokeRepeating("Blink", 0f, 0.25f);
            Invoke("RestoreTrail", 3.2f);
        }
    }
    
    private void OnCollisionEnter(Collision o)
    {
        if (!isServer)
        {
            return;
        }
        if (o.gameObject.GetComponent<GamePlayerInfo>() != null)
        {
            lastHit = o.gameObject.GetComponent<GamePlayerInfo>();
        }
    }

    private void RestoreTrail()
    {
        trail.GetComponent<TrailRenderer>().time = 5;
    }

    [Command]
    public void CmdApplyExplosionForce(float power, Vector3 pos, float radius, float upforce)
    {
        thisRG.AddExplosionForce(power, pos, radius, upforce, ForceMode.Impulse);
    }

    [ClientRpc]
    public void RpcApplyExplosionForce(float power, Vector3 pos, float radius, float upforce)
    {
        if (isLocalPlayer)
        {
            thisRG.AddExplosionForce(power, pos, radius, upforce, ForceMode.Impulse);
        }
    }

    private void Blink()
    {
        t -= 0.25f;
        MeshRenderer[] allmesh = gameObject.GetComponentsInChildren<MeshRenderer>();
        if (t <= 0)
        {
            foreach (MeshRenderer m in allmesh)
            {
                m.enabled = true;
            }
            displaying = true;
            t = 3;
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            transform.position = spawnPoint;
            thisRG.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            transform.LookAt(new Vector3(0, transform.position.y, 0));
            CancelInvoke("Blink");
            isSpawning = false;
        }
        else
        {
            foreach (MeshRenderer m in allmesh)
            {
                m.enabled = !displaying;
            }
            displaying = !displaying;
            Debug.Log("Time " + t);
        }
    }
    
    [Command]
    public void CmdRespawn()
    {
        if (isSpawning)
        {
            return;
        }
        if (lastHit != null)
        {
            lastHit.score++;
            Leaderboard.instance.AddScore(playerName, 1);
            lastHit = null;
        }
        else
        {
            score--;
            Leaderboard.instance.AddScore(playerName, -1);
        }
        RpcRespawn();
    }
}
