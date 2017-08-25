using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform cameraAt;
    public GameObject Player;
	public int Speed = 5;

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
		transform.RotateAround(Player.transform.position, Vector3.up, Speed * Input.GetAxis("Mouse X"));
		transform.RotateAround(Player.transform.position, Vector3.right, Speed * Input.GetAxis("Mouse Y"));
    }
}
