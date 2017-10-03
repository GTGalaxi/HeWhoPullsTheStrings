using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform cameraAt;
    public GameObject rotator;
    public GameObject player;
    public GameObject hitBox;
    public GameObject scarabObject;
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

        Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Direction = Camera.main.transform.TransformDirection(Direction);

        if (!scarab)
        {
            print(false);
            rotator.transform.RotateAround(rotator.transform.position, rotator.transform.up, Speed * Input.GetAxis("Mouse X"));
            transform.RotateAround(rotator.transform.position, rotator.transform.right, Speed * Input.GetAxis("Mouse Y"));
            float temp = Mathf.Clamp(transform.eulerAngles.x, -70, 70);
            transform.eulerAngles.Set(temp, transform.rotation.y, transform.rotation.z);
            print(Mathf.Clamp(transform.eulerAngles.x, -70F, 70F));
        }
        else
        {
            print(true);
            ScarabVision();
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
            transform.eulerAngles.Set(transform.rotation.x, transform.rotation.y, rotator.transform.rotation.z);
            scarabObject.transform.position = (scarabObject.transform.position + Direction * Time.deltaTime);
            transform.position = scarabObject.transform.position;
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
            transform.eulerAngles.Set(transform.rotation.x, transform.rotation.y, rotator.transform.rotation.z);

        }
    }
}
