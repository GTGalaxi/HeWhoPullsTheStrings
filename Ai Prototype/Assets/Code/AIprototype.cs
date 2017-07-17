using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIprototype : MonoBehaviour
{







    public Transform player;
    public Transform head;



    public float Following = 0.0f;
    public float Followingt = 0.0f;
   public float Degreesight = 30;
  public float SpeedToTarget = 0.0f;
    public float RotationToTarget;


    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;


    public float RTWSpeed = 0.0f;
    public float WTWSpeed = 0.0f;
    public float accuarcyWP = 0.0f;


    bool targeted = false;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {

        Ai();
        LookAt();
    }




    void Ai()
    {


        if (Vector3.Distance(player.position, this.transform.position) < Following)
        {
            targeted = true;
           

        }


        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, this.head.up);


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
            this.transform.Translate(0, 0, Time.deltaTime * WTWSpeed);
        }


  

    }

    void LookAt()
    {

        


        if (Vector3.Distance(player.position, this.transform.position) < Followingt)
        {
            targeted = true;
            
            GetComponent<Renderer>().material.color = new Color(0, 110, 0);

            if (targeted == true)
            {

                Vector3 direction = player.position - this.transform.position;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), RotationToTarget);
                float angle = Vector3.Angle(direction, this.head.up);
                this.transform.Translate(0, 0, Time.deltaTime * SpeedToTarget);


                // if (direction.magnitude > Attacking)
                // {

                //  this.transform.Translate(0, 0, SpeedToTarget);


            }

        }

        if (Vector3.Distance(player.position, this.transform.position) > Followingt)
        {
            targeted = false;
            GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }

       
            
    }




        



























































 }

