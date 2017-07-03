using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform cameraAt;
    public GameObject Player;

    void OnEnable()
    {
        transform.position = cameraAt.position;
    }

    void Start ()
    {
        transform.position = cameraAt.position;
    }
	
	void FixedUpdate ()
    {
        transform.LookAt(Player.transform);
        transform.RotateAround(Player.transform.position, Vector3.up, 20 * Input.GetAxis("Mouse X"));
    }
}
