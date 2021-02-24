using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisableOnground : NetworkBehaviour {

    [ServerCallback]
    private void OnCollisionEnter(Collision o)
    {
        if (!isServer)
        {
            return;
        }
        if (o.gameObject.tag == "Player")
        {
            o.gameObject.GetComponent<MyPlayerController>().onGround = true;
        }
    }

    [ServerCallback]
    private void OnCollisionExit(Collision o)
    {
        if (!isServer)
        {
            return;
        }
        if (o.gameObject.tag == "Player")
        {
            o.gameObject.GetComponent<MyPlayerController>().onGround = false;
        }
    }

}
