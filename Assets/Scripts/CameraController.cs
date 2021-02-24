using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float height = .5f;
    public float radius = -1.5f;
    public float cameraAngle = 22.5f;
    public float calibrate;
    private Vector3 offsetPosition;
    private Vector3 offsetRotation;

    private void Start()
    {
        //offsetPosition = new Vector3(0, radius, height);
        offsetRotation = new Vector3(cameraAngle, calibrate, 0);
        transform.rotation = Quaternion.Euler(offsetRotation + target.transform.rotation.eulerAngles);
    }

    public void MoveCamera()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(offsetRotation + target.transform.rotation.eulerAngles), Time.deltaTime);
        Vector3 tmpPosition = new Vector3(Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * radius, height, Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * radius) + target.transform.position;
        transform.position = tmpPosition;
    }

}
