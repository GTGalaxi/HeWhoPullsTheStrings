using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform cameraAt;
    public GameObject rotator;
    public GameObject player;
    public GameObject hitBox;
    public GameObject scarabObject;
    public GameObject cameraXRotator;
	public int Speed = 5;
    public bool scarab = false;
    public float timer = 0;
    public float smoothTime = 5f;

    private Quaternion cameraTargetRotX;
    private Quaternion cameraTargetRotY;
    public float xSensitivity = 2f;
    public float ySensitivity = 4f;
    public bool smooth = true;

    void OnEnable()
    {
        transform.position = cameraAt.position;
    }

    void Start ()
    {
        cameraTargetRotX = cameraXRotator.transform.localRotation;
        cameraTargetRotY = rotator.transform.localRotation;
        transform.LookAt(rotator.transform);
        transform.position = cameraAt.position;
    }
	
	void FixedUpdate ()
    {

        float yRot = Input.GetAxis("Mouse X") * ySensitivity;
        float xRot = Input.GetAxis("Mouse Y") * xSensitivity;
        cameraTargetRotX *= Quaternion.Euler (xRot, 0f, 0f);
        cameraTargetRotY *= Quaternion.Euler(0f, yRot, 0f);

        if (!scarab)
        {
            cameraTargetRotX = ClampRotationAroundXAxis(cameraTargetRotX);

            if (smooth)
            {
                rotator.transform.localRotation = Quaternion.Slerp(rotator.transform.localRotation, cameraTargetRotY, smoothTime * Time.deltaTime);
                cameraXRotator.transform.localRotation = Quaternion.Slerp(cameraXRotator.transform.localRotation, cameraTargetRotX, smoothTime * Time.deltaTime);
            }
            else
            {
                rotator.transform.localRotation = cameraTargetRotY;
                cameraXRotator.transform.localRotation = cameraTargetRotX;

            }
        }

        else
        {
            Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            Direction = Camera.main.transform.TransformDirection(Direction);
            
            ScarabVision();
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
            transform.eulerAngles.Set(transform.rotation.x, transform.rotation.y, rotator.transform.rotation.z);
            scarabObject.transform.position = (scarabObject.transform.position + Direction * 3 * Time.deltaTime);
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
            timer = 0;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -20F, 60F);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
