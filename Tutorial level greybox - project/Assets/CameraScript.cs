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

        transform.LookAt(Player.transform);
        transform.position = cameraAt.position;
    }
	
	void Update ()
    {
        transform.RotateAround(Player.transform.position, Player.transform.up, Speed * Input.GetAxis("Mouse X"));
        Player.transform.RotateAround(Player.transform.position, Player.transform.up, Speed * Input.GetAxis("Mouse X"));
		transform.RotateAround(Player.transform.position, Player.transform.right, Speed * Input.GetAxis("Mouse Y"));
        transform.eulerAngles.Set(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }
}
