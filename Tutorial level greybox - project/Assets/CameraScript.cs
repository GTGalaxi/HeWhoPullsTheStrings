using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform cameraAt;
    public GameObject rotator;
    public GameObject player;
    public GameObject hitBox;
	public int Speed = 5;
    public bool scarab = false;
    public float timer = 0;

    void OnEnable()
    {
        transform.position = cameraAt.position;
    }

    void Start ()
    {

        transform.LookAt(rotator.transform);
        transform.position = cameraAt.position;
    }
	
	void Update ()
    {
        if (!scarab)
        {
            print(false);
            transform.RotateAround(rotator.transform.position, rotator.transform.up, Speed * Input.GetAxis("Mouse X"));
            rotator.transform.RotateAround(rotator.transform.position, rotator.transform.up, Speed * Input.GetAxis("Mouse X"));
            transform.RotateAround(rotator.transform.position, rotator.transform.right, Speed * Input.GetAxis("Mouse Y"));
            transform.eulerAngles.Set(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        else
        {
            print(true);
            ScarabVision();
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
            transform.localRotation.Set(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
        }


        if (Input.GetKey(KeyCode.T))
        {
            scarab = true;
        }
    }
        

    void ScarabVision()
    {
        timer += Time.deltaTime;
        hitBox.SetActive(false);
        player.GetComponent<Player_Movement>().enabled = false;
        if (timer > 10 && scarab == true)
        {
            rotator.transform.rotation.Set(0, 0, 0, 0);
            scarab = false;
            hitBox.SetActive(true);
            player.GetComponent<Player_Movement>().enabled = true;
            transform.position = cameraAt.transform.position;
            transform.LookAt(rotator.transform);
            transform.localRotation.Set(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);

        }
    }
}
