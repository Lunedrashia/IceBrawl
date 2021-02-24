using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBumper : MonoBehaviour {

    public Rigidbody thisRigid;
    public Collider playerCollider;
    public float defaultBounciness;
    public float adjustedBounciness;

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision o)
    {
        if (o.gameObject.tag == "Player")
        {
            playerCollider.material.bounciness = adjustedBounciness;
            foreach (ContactPoint contact in o.contacts)
            {
                thisRigid.AddExplosionForce(10, contact.point, 0.1f);
            }
        }
    }

    private void OnCollisionExit(Collision o)
    {
        playerCollider.material.bounciness = defaultBounciness;
    }
}
