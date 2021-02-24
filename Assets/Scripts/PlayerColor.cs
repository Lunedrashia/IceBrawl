using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerColor : NetworkBehaviour {

    [SyncVar] public Color color;
    public GameObject[] mesh;
    public GameObject trail;

    // Use this for initialization
    void Start () {
        trail.GetComponent<TrailRenderer>().startColor = color;
        trail.GetComponent<TrailRenderer>().endColor = color;
        foreach (GameObject m in mesh)
        {
            m.GetComponent<MeshRenderer>().material.color = color;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
