using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Variables
{
    [HideInInspector]
    public Rigidbody player;
    public bool grounded = true;
	public GameObject ChildModel;
	public Vector3 CurrentRotation;

    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float sprintSpeed = 15f;
    public float jumpForce = 5f;
    [Range(0, 360)]
    [Tooltip("Rotation in Degrees/s")]
    public float rotateSpeed = 100f;

    public Animation anim;
    public AnimationClip runAnim;
    public AnimationClip breathingAnim;
}

public class Player_Movement : MonoBehaviour
{


    public Variables Variables = new Variables();

    // Use this for initialization
    void Start()
    {
        Variables.anim = GetComponent<Animation>();
        Variables.player = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		Vector3 Direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical"));
		Direction = Camera.main.transform.TransformDirection (Direction);
		Direction.y = 0f;
		Vector3 currentPos = Variables.player.transform.position;
		if (Input.GetAxis ("Jump") == 1) {
			if (Variables.grounded == true) {
				Variables.grounded = false;
				Variables.player.velocity = (new Vector3 (0f, Input.GetAxis ("Jump") * Variables.jumpForce, 0f));

			}
		}
        if (!Input.GetKey(KeyCode.Q))
        {
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
                Variables.anim.clip = Variables.runAnim;
                if (!Variables.anim.IsPlaying(Variables.runAnim.name))
                {
                    Variables.anim.CrossFade(Variables.runAnim.name, 0.2F, PlayMode.StopAll);
                }
                Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                Movement = Camera.main.transform.TransformDirection(Movement);
                Movement.y = 0f;
                Variables.ChildModel.transform.rotation = Quaternion.LookRotation(Movement) * Quaternion.Inverse(Quaternion.Euler(0f, 0f, 0f));
                Variables.CurrentRotation = Variables.ChildModel.transform.eulerAngles;
            }
            else
            {
                Variables.anim.clip = Variables.breathingAnim;
                if (!Variables.anim.IsPlaying(Variables.breathingAnim.name))
                {
                    Variables.anim.CrossFade(Variables.breathingAnim.name, 0.2F, PlayMode.StopAll);
                }
                Variables.ChildModel.transform.eulerAngles = Variables.CurrentRotation;
            }
        }
        else
        {
            Variables.anim.clip = Variables.breathingAnim;
            if (!Variables.anim.IsPlaying(Variables.breathingAnim.name))
            {
                Variables.anim.CrossFade(Variables.breathingAnim.name, 0.2F, PlayMode.StopAll);
            }
            Variables.ChildModel.transform.eulerAngles = Variables.CurrentRotation;
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