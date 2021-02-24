using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RespawnWhenFall : NetworkBehaviour {

    private void OnTriggerStay(Collider o)
    {
        if (!isServer)
        {
            return;
        } 

        if(o.gameObject.tag == "Player")
        {
            o.gameObject.GetComponent<GamePlayerInfo>().CmdRespawn();
        }
        else
        {
            Destroy(o.gameObject);
        }
    }
}
