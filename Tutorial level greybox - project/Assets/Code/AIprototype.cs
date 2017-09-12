using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIprototype : MonoBehaviour
{





   
   

    public Transform player;
    public Transform head;

    //private NavMeshAgent NavComponent;

    //public float Following = 0.0f;
    public float Followingt = 0.0f;
   //public float Degreesight = 30;
    public float inView = 20.0f;
    // public float SpeedToTarget = 0.0f;
    public float RotationToTarget;


    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;


    public float RTWSpeed = 0.0f;
    public float WTWPSpeed = 0.0f;
   // public double WalkSpeed = 0.12f;
    public float accuarcyWP = 0.0f;


    

    bool targeted = false;



    // Use this for initialization
    void Start()
    {

       // NavMeshAgent agent = GetComponent<NavMeshAgent>();
       

    }

    // Update is called once per frame
    void Update()

    {
       
        Ai();
        LookAt();
    }




    void Ai()
    {

       
        Vector3 Join = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, Join);
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        if (angle < inView)
        {
            targeted = true;
           

        }

        if (angle > inView)
        {
            targeted = false;


        }

        if (targeted == false)
        {
           

            if (state == "patrol" && waypoints.Length > 0)
            {

                if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuarcyWP)
                {
                  

                    //currentWP = Random.Range (0, waypoints.Length);

                    //one after another 
                    currentWP++;
                    if (currentWP >= waypoints.Length)
                    {
                        currentWP = 0;
                    }
                }
                direction = waypoints[currentWP].transform.position - transform.position;
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), RTWSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, Time.deltaTime * WTWPSpeed);
                if (targeted == true)
                {
                    WTWPSpeed = 0;
                }
            }

            LookAt();



        }

       


  

    }

    void LookAt()
    {

        if (targeted == true)
        {
            
            
            GetComponent<Renderer>().material.color = new Color(0, 110, 0);

            

                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), RotationToTarget);
                //float angle = Vector3.Angle(direction, this.head.up);
               // this.transform.Translate(0, 0, 0);


                


            

        }

         if (targeted == false)
        {
            targeted = false;
            GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }



    }








   
 


  
       
       
       






     
















































}

