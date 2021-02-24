using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float duration = 1f;
    private float counter = 0f;
    private bool started = false;
    public bool selfDestroy = true;
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            counter += Time.deltaTime;
            if (counter >= duration)
            {
                if (selfDestroy)
                {
                    Destroy(this);
                }
                else
                {
                    ResetTimer();
                }
            }
        }
	}

    public void StartTimer()
    {
        started = true;
    }

    public void ResetTimer()
    {
        started = false;
        counter = 0f;
    }

    public void AdjustDuration(float t)
    {
        duration += t;
    }

    public bool isStarted()
    {
        return started;
    }
}
