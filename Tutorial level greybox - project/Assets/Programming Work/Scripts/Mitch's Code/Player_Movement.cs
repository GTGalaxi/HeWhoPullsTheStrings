using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Variables
{
    [HideInInspector]
    public Rigidbody player;
    public bool grounded = true;
	public GameObject ChildModel;
	public Vector3 CurrentRotation;

    [Header("Movement")]
    public float forwardSpeed = 8f;
    public float sprintSpeed = 15f;
    public float jumpForce = 5f;
    [Range(0, 360)]
    [Tooltip("Rotation in Degrees/s")]
    public float rotateSpeed = 100f;

    public Animation anim;
    public AnimationClip runAnim;
    public AnimationClip crouchAnim;
    public AnimationClip crouchIdleAnim;
    public AnimationClip breathingAnim;

}
public class Player_Movement : MonoBehaviour
{
    public float colliderPos;
    public float colliderHeight;
    public bool jumpAllowed = false;
    public Variables Variables = new Variables();
    public GameObject thingo;
    public RuneInventory runeInventory;
	public Fungus.Flowchart FungusStuff;
	public bool FungusWait = false;
    public bool possessing = false;

    // Use this for initialization
    void Start()
    {
        
        Variables.anim = GetComponent<Animation>();
        Variables.player = gameObject.GetComponent<Rigidbody>();
        colliderPos = GetComponent<CapsuleCollider>().center.y;
        colliderHeight = GetComponent<CapsuleCollider>().height;
        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
		if (FungusStuff != null) 
		{
			FungusWait = FungusStuff.GetBooleanVariable ("Wait");
		}
    }

    // Update is called once per frame
    void FixedUpdate()
	{
        float colliderHeightTemp = colliderHeight / 1.7f;

        if (Input.GetAxis("Crouch") == 1)
        {
            Variables.forwardSpeed = 3f;
            GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0f);
            GetComponent<CapsuleCollider>().height = colliderHeightTemp;
        }
        else
        {
            Variables.forwardSpeed = 8f;
            GetComponent<CapsuleCollider>().center = new Vector3(0, colliderPos, 0);
            GetComponent<CapsuleCollider>().height = colliderHeight;

        }
		Vector3 Direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0f, Input.GetAxisRaw ("Vertical"));
		Direction = thingo.transform.TransformDirection (Direction);
		Direction.y = 0f;
		Vector3 currentPos = Variables.player.transform.position;
		if (Input.GetAxis ("Jump") == 1 && jumpAllowed == true) {
			if (Variables.grounded == true) {
				Variables.grounded = false;
				Variables.player.velocity = (new Vector3 (0f, Input.GetAxis ("Jump") * Variables.jumpForce, 0f));

			}
		}
		if (!runeInventory.pause && FungusWait == false && possessing == false)
        {
            if (Input.GetKey("right shift") || Input.GetKey("left shift") || Input.GetKey("joystick button 10"))
            {
                Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.sprintSpeed);

            }
            else
            {
                Variables.player.position = (transform.position + Direction * Time.deltaTime * Variables.forwardSpeed);
            }

            if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
            {
                if (Input.GetAxis("Crouch") == 1)
                {
                    Variables.anim.clip = Variables.crouchAnim;
                    Variables.anim["CrouchWalk"].speed = 3;
                    if (!Variables.anim.IsPlaying(Variables.crouchAnim.name))
                    {
                        Variables.anim.CrossFade(Variables.crouchAnim.name, 0.2F, PlayMode.StopAll);
                    }
                }
                else
                {
                    Variables.anim.clip = Variables.runAnim;
                    if (!Variables.anim.IsPlaying(Variables.runAnim.name))
                    {
                        Variables.anim.CrossFade(Variables.runAnim.name, 0.2F, PlayMode.StopAll);
                    }

                }
                Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                Movement = Camera.main.transform.TransformDirection(Movement);
                Movement.y = 0f;
                Variables.ChildModel.transform.rotation = Quaternion.LookRotation(Movement) * Quaternion.Inverse(Quaternion.Euler(0f, 0f, 0f));
                Variables.CurrentRotation = Variables.ChildModel.transform.eulerAngles;
            }
            else
            {
                if (Input.GetAxis("Crouch") == 1)
                {
                    Variables.anim.clip = Variables.crouchIdleAnim;
                    if (!Variables.anim.IsPlaying(Variables.crouchIdleAnim.name))
                    {
                        Variables.anim.CrossFade(Variables.crouchIdleAnim.name, 0.2F, PlayMode.StopAll);
                    }
                    Variables.ChildModel.transform.eulerAngles = Variables.CurrentRotation;
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
    }

    void OnCollisionExit(Collision other)
    {
        Variables.grounded = false;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Verticality")
        {
            jumpAllowed = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Verticality")
        {
            jumpAllowed = false;
        }
    }
}