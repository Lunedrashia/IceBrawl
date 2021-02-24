using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOff : MonoBehaviour {

    public GameObject explosionPoint;
    public float power;
    public float radius;
    public Rigidbody player;
    public Timer timer;
    public bool avaliable = true;

    private void OnCollisionEnter(Collision o)
    {
        if (o.gameObject.tag == "Player" && GameManager.instance.countdown <= 0)
        {
            player = o.gameObject.GetComponent<Rigidbody>();
            player.AddExplosionForce(power, explosionPoint.transform.position, radius);
            timer.StartTimer();
        }
    }

    public void Use()
    {
        avaliable = false;
        Invoke("CanUse", timer.duration * 2);
    }

    public void CanUse()
    {
        avaliable = true;
    }
}
