using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyPlayerController : NetworkBehaviour {

    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public Vector3 maxSpeed;
    [SerializeField] public bool onGround = true;

    [Header("Camera property")]
    [SerializeField] public float height = .5f;
    [SerializeField] public float radius = -1.5f;
    [SerializeField] public float cameraAngle = 22.5f;
    [SerializeField] public float calibrate;
    private Vector3 offsetPosition;
    private Vector3 offsetRotation;

    public Rigidbody rigid;
    private Transform thisCamera;
    public GameObject bullet;
    public Transform cannonSpawnPoint;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        offsetRotation = new Vector3(cameraAngle, calibrate, 0);
        offsetPosition = new Vector3(0, radius, height);
        thisCamera = Camera.main.transform;
        thisCamera.position = transform.position + offsetPosition;
        thisCamera.rotation = Quaternion.Euler(offsetRotation + transform.rotation.eulerAngles);
        MoveCamera();
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        if (GameManager.instance.countdown == 0)
        {
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        if (GameManager.instance.countdown <= 0 && GameManager.instance.gameTimer > 0)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(0f, 0f, moveVertical);
            Vector3 rotate = new Vector3(0f, moveHorizontal, 0f);
            transform.Rotate(rotate);
            if (onGround && rigid.velocity.x <= maxSpeed.x && rigid.velocity.y <= maxSpeed.y)
            {
                rigid.AddRelativeForce(movement * moveSpeed);
            }
            MoveCamera();
        }
        else
        {
            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }

	}

    public void MoveCamera()
    {
        //thisCamera.rotation = Quaternion.Lerp(thisCamera.rotation, Quaternion.Euler(offsetRotation + transform.rotation.eulerAngles), Time.fixedDeltaTime);
        thisCamera.rotation = Quaternion.Euler(offsetRotation + transform.rotation.eulerAngles);
        Vector3 tmpPosition = new Vector3(Mathf.Sin(thisCamera.rotation.eulerAngles.y * Mathf.Deg2Rad) * radius, height, Mathf.Cos(thisCamera.rotation.eulerAngles.y * Mathf.Deg2Rad) * radius) + transform.position;
        thisCamera.position = tmpPosition;
    }
}
