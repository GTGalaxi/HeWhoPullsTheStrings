using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyAi : MonoBehaviour {

    //Variable for patrolling
    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 0.5f;
    // public float rotSpeed = 0.2f;
    public float Inview = 20f;
    public float rotspeed = 0.5f;


    //Variables for Finding player
    public Transform target;
    public GameObject player;
    //public float hurttimer = 5;


    ////Variables for sight
    public float heightMultiplier;
   // public float CannotSee = 10;
    public float CanSee = 5;


    public RuneInventory runeInventory;


    public bool reverse = false;




    // checks if player hase been seen
    public bool Seen = false;
    public bool HurtPlayer = false;

    public bool temp = false;
    public bool cooldown = false;
    public float cooldownTimer = 20f;
    public float possessionTimer = 5f;



    // Use this for initialization
    void Start () {
        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
        Patrol();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && runeInventory.hoveredRune == 4 && cooldown == false)
        {
            temp = true;
            cooldown = true;
            cooldownTimer = 20f;
            possessionTimer = 5f;
        }
        if (cooldown == true)
        {
            cooldownTimer = cooldownTimer - Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                cooldown = false;
            }
        }
        else
        {
            temp = false;

        }
        if (temp == true && possessionTimer >= 0)
        {
            Vector3 Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            Direction = Camera.main.transform.TransformDirection(Direction);
            Direction.y = 0f;
            transform.position = (transform.position + Direction * Time.deltaTime * 6);
            possessionTimer = possessionTimer - Time.deltaTime;
            player.GetComponent<Player_Movement>().enabled = false;
        }
        else
        {
            player.GetComponent<Player_Movement>().enabled = true;
            Patrol();
        }
	}


    void Patrol()
    {

        Vector3 distance = target.position - this.transform.position;
        Vector3 Join = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, Join);


       

        Vector3 LookPos = waypoints[waypointInd].transform.position;
        LookPos.y = transform.position.y;
        transform.LookAt(LookPos);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookPos), rotspeed * Time.deltaTime);




        if (Seen ==false)
        { 
            if (Vector3.Distance(waypoints[waypointInd].transform.position, transform.position) <= 2)
            {
                if (reverse == false)
                {
                    waypointInd++;
                    if (waypointInd >= waypoints.Length)
                    {
                        waypointInd--;
                        reverse = true;
                    }
                }
                else
                {
                    waypointInd--;
                    if (waypointInd == 0)
                    {
                        reverse = false;
                    }
                }
            }
            //speed of pathing as well as rotation to waypoints.
            distance = waypoints[waypointInd].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance), rotspeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * patrolSpeed);

        }
      
       
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && runeInventory.hoveredRune == 4)
        {

        }
        else
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * CanSee, Color.green);

            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CanSee))
            {
                // not sure whats happening, ai is ignoring the hit.collider.gameobject.tag.
                //
                //
                //
                //
                //
                // this debug works
                //Debug.Log("Hello ray works");

                //this part is being ignored for reasons unknown. player character is tagged as player.
                if (hit.collider.gameObject.tag == "Player")
                {

                    // this debug does not work.
                    //
                    //not sure why 
                    Debug.Log("Hit player");
                    player = hit.collider.gameObject;
                    Seen = true;

                    if (Seen == true)
                    {

                        Vector3 LookPos = target.transform.position;
                        LookPos.y = transform.position.y;
                        transform.LookAt(LookPos);
                        HurtPlayer = true;

                        patrolSpeed = 0;

                        if (HurtPlayer == true)
                        {

                            Destroy(player);
                        }
                    }
                }
            }
        }
    }
}
