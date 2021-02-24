using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CannonballExplode : NetworkBehaviour {

    [SerializeField] public GamePlayerInfo user;
    [SerializeField] public Rigidbody thisRigid;
    [SerializeField] public float power;
    [SerializeField] public float radius;
    [SerializeField] public float upforce;
    [SerializeField] public float lifetime = 5f;
    [SerializeField] private bool alive = true;

    [ServerCallback]
    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 || !alive)
        {
            Destroy(gameObject);
        }
    }

    private void Bombing(Collision o)
    {
        if (!alive)
        {
            return;
        }

        Debug.Log(o.gameObject.name);

        if (o.gameObject.GetComponent<GamePlayerInfo>() != null)
        {
            if (o.gameObject.GetComponent<GamePlayerInfo>() == user)
            {
                o.gameObject.GetComponent<GamePlayerInfo>().lastHit = null;
                Debug.Log("Hit yourself");
            }
            else
            {
                o.gameObject.GetComponent<GamePlayerInfo>().lastHit = user;
                //user.score++;
                Debug.Log("Set lasthit");
            }
            //c.gameObject.GetComponent<GamePlayerInfo>().CmdApplyExplosionForce(power, transform.position, radius, upforce);
        }
        gameObject.GetComponent<Renderer>().enabled = false;

        if (!isServer)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders)
        {
            /*
            Rigidbody target = c.GetComponent<Rigidbody>();
            if (target != null)
            {
                if (c.gameObject.GetComponent<GamePlayerInfo>() != null)
                {
                    if (c.gameObject.GetComponent<GamePlayerInfo>() == user)
                    {
                        c.gameObject.GetComponent<GamePlayerInfo>().lastHit = null;
                    }
                    else
                    {
                        c.gameObject.GetComponent<GamePlayerInfo>().lastHit = user;
                        //user.score++;
                    }
                    //c.gameObject.GetComponent<GamePlayerInfo>().CmdApplyExplosionForce(power, transform.position, radius, upforce);
                }
            }
            */
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision o)
    {
        Bombing(o);
    }
}
