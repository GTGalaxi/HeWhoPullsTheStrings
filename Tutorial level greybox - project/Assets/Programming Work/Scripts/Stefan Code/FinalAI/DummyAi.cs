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





    public bool reverse = false;




    // checks if player hase been seen
    public bool Seen = false;
    public bool HurtPlayer = false;




    // Use this for initialization
    void Start () {

        Patrol();

        

    }
	
	// Update is called once per frame
	void Update () {



        FixedUpdate();
        Patrol();
		
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
            if (Vector3.Distance(waypoints[waypointInd].transform.position, transform.position) < 0)
            {
                waypointInd++;
                if (waypointInd >= waypoints.Length)
                {
                    waypointInd = 0;
                }

            }
            // affects how close ai goes to way point as well as reverses pathing movement.
            else if (Vector3.Distance(waypoints[waypointInd].transform.position, transform.position) <= 2)
            {
                if (reverse == false)
                {
                    waypointInd++;
                    if (waypointInd >= waypoints.Length)
                    {
                        reverse = true;
                    }
                }
                if (reverse == true)
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
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * CanSee, Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CanSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
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
            else
            {
                Seen = false;
                HurtPlayer = false;



            }
        }





















    }
}
