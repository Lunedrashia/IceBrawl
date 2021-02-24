using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform cannonSpawnPoint;
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameTimer > 0)
        {
            CmdFire();
        }
    }

    [Command]
    public void CmdFire()
    {
        RpcFire();
    }

    [ClientRpc]
    public void RpcFire()
    {
        GameObject RB = (GameObject)Instantiate(bullet, cannonSpawnPoint.position, transform.rotation);
        RB.GetComponent<CannonballExplode>().user = gameObject.GetComponent<GamePlayerInfo>();
        RB.GetComponent<Rigidbody>().velocity = RB.transform.forward * 250;
        NetworkServer.Spawn(RB);
    }
}
