using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.LookAt(new Vector3(0, transform.position.y, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        transform.LookAt(new Vector3(0, transform.position.y, 0));
    }
}
