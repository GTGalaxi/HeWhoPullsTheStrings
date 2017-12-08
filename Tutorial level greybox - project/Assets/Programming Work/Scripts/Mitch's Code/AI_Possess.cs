using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Possess : MonoBehaviour
{

	[HideInInspector]
	public Rigidbody AI;
	public Player_Movement Player;
	public GameObject ChildModel;
	public Vector3 CurrentRotation;

	[Header("Movement")]
	public float forwardSpeed = 10f;
	public float sprintSpeed = 15f;
	[Range(0, 360)]
	[Tooltip("Rotation in Degrees/s")]
	public float rotateSpeed = 100f;

    public RuneInventory runeInventory;
    public GameObject thingo;
    public GameObject CameraAtAt;
    public float timer = 0;
    public float possessionTime = 10;
    public Script_General_AI script_AI_Test;
    public MagnetCollision magnet;
    public bool able = false;
    public GameObject hitBox;

    // Use this for initialization
    void Awake()
	{
		enabled = false;
	}
    
    // Use this for initialization
    void Start()
    {
        magnet = GameObject.Find("Hitbox").GetComponent<MagnetCollision>();
        CameraAtAt = GameObject.Find("CameraAtAt");
        thingo = GameObject.Find("CameraLook");
        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
        AI = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
	{
        hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();

        if (timer <= 0.0f && runeInventory.hoveredRune == 4 && hitBoxScript.HitTarget == true && able == false)
        {
            able = true;
        }

        if (able)
        {
            if (timer <= possessionTime)
            {
                CameraAtAt.transform.position = this.transform.position;
                timer += Time.deltaTime;
                Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                Direction = thingo.transform.TransformDirection(Direction);
                Direction.y = 0f;
                Vector3 currentPos = AI.transform.position;

                if (Input.GetKey("right shift") || Input.GetKey("left shift") || Input.GetKey("joystick button 8"))
                {

                    AI.position = (transform.position + Direction * Time.deltaTime * sprintSpeed);

                }
                else
                {
                    AI.position = (transform.position + Direction * Time.deltaTime * forwardSpeed);
                }

                if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
                {
                    Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                    Movement = Camera.main.transform.TransformDirection(Movement);
                    Movement.y = 0f;
                    ChildModel.transform.rotation = Quaternion.LookRotation(Movement) * Quaternion.Inverse(Quaternion.Euler(0f, 0f, 0f));
                    CurrentRotation = ChildModel.transform.eulerAngles;
                }
                else
                {
                    ChildModel.transform.eulerAngles = CurrentRotation;
                }
            }
            else
            {
                CameraAtAt.transform.position = this.transform.position;
                Player.GetComponent<Player_Movement>().enabled = true;
                this.GetComponent<AI_Possess>().enabled = false;
            }
        }
        
	}
}