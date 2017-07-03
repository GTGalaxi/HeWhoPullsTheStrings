using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Variables
{
    [HideInInspector]
    public Rigidbody player;
    public bool grounded = true;

    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float sprintSpeed = 15f;
    public float jumpForce = 5f;
    [Range(0, 360)]
    [Tooltip("Rotation in Degrees/s")]
    public float rotateSpeed = 100f;
}

public class Player_Movement : MonoBehaviour
{


    public Variables Variables = new Variables();

    // Use this for initialization
    void Start()
    {
        Variables.player = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Direction = Camera.main.transform.TransformDirection(Direction);
        Direction.y = 0f;
        Vector3 currentPos = Variables.player.transform.position;

        //commented out new changes to accomadate current level that need old movement;


        if (Input.GetAxis("Jump") == 1)
        {
            if (Variables.grounded == true)
            {

                Variables.grounded = false;

                Variables.player.velocity = (new Vector3(0f, Input.GetAxis("Jump") * Variables.jumpForce, 0f));

            }
        }

        if (Input.GetKey("right shift") || Input.GetKey("left shift") || Input.GetKey("joystick button 8"))
        {

            Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.sprintSpeed);

        }

        else
        {
            Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.forwardSpeed);
        }

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            Movement = Camera.main.transform.TransformDirection(Movement);
            Movement.y = 0f;
        }
    }

    void OnCollisionStay(Collision col)
    {
        Variables.grounded = true;
        if (col.gameObject.tag == "notGround")
        {
            Variables.grounded = false;
        }

        if (col.transform.tag == "MovingPlatform")
        {
            transform.parent = col.transform;
        }
    }

    void OnCollisionExit(Collision other)
    {
        Variables.grounded = false;
    }

    void OnTriggerEnter(Collider col)
    {
    }
}