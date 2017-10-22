﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable]
public class AnimationSetMaid
{
    public Animation anim;
    public AnimationClip MaidWalk;
    public AnimationClip MaidInvestigate;
    public AnimationClip MaidHurt;
}










public class Maid_AI : MonoBehaviour
{





    public AnimationSetMaid AnimationSetMaid = new AnimationSetMaid();




    public NavMeshAgent agent;
    //   public GameObject AI;

    public enum State
    {
        PATROL,
        HURT,
        INVESTIGATE


    }

    public State state;
    private bool alive;

    //Variable for patrolling
    public GameObject[] waypoints;
    private int waypointInd = 0;
    public float patrolSpeed = 0.5f;
    // public float rotSpeed = 0.2f;
    public float chasespeed = 1f;


    //Variables for Finding player
    public GameObject target;
    public float hurttimer = 5;
    //Variables for sight
    public float heightMultiplier;
    public float CannotSee = 10;
    public float CanSee = 5;
    //Variables for investigating
    private Vector3 investigateSpot;
    private float timer = 0;
    public float investigateWait = 10;
    public float Killingplayer = 3;






    // changes behaviour of ai depending on stages
    public Material[] material;
    Renderer rend;
    // checks if player hase been seen
    public bool Seen = false;
    private bool OutofRange = false;
    public bool HurtPlayer = false;

    // Use this for initialization
    void Start()
    {

        AnimationSetMaid.anim = GetComponent<Animation>();


        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        // character.GetComponent<ImmRune>().KnockOutRune();
        //ImmRune KO = gameObject.GetComponent<ImmRune>();

        agent = GetComponent<NavMeshAgent>();


        agent.updatePosition = true;

        agent.updateRotation = true;

        //waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        //waypointInd = Random.Range(0, waypoints.Length);
        state = Maid_AI.State.PATROL;

        alive = true;
        // Start FSM
        StartCoroutine(FSM());

        heightMultiplier = 1.36f;



    }
    //void Awake()
    //{
    //    AI = transform;
    //}

    void Update()
    {


        agent = GetComponent<NavMeshAgent>();


    }
    /// <summary>
    // waitto blue

    // the project factorie
    /// </summary>
    /// <returns></returns>

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {

                case State.PATROL:
                    Patrol();
                    break;
                case State.HURT:
                    Hurt();
                    break;
                case State.INVESTIGATE:
                    Investigate();
                    break;

            }
            yield return null;
        }
    }

    void Patrol()
    {



        rend.sharedMaterial = material[0];

        Debug.Log("going to checkpoint");
        agent.speed = patrolSpeed;



        //Vector3 direction = transform.position - this.transform.position;
        //direction.y = 0;



        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) > 5)
        {
            //Vector3 LookPos = waypoints[waypointInd].transform.position;
            //LookPos.y = transform.position.y;
            //transform.LookAt(LookPos);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookPos), rotSpeed * Time.deltaTime);


            //waypointInd++;
            //if (waypointInd >= waypoints.Length)
            //{
            //    waypointInd = 0;

            //}
            agent.SetDestination(waypoints[waypointInd].transform.position);



            AnimationSetMaid.anim.clip = AnimationSetMaid.MaidWalk;

            //  Don't use anim.Play as you can't blend anims
            //  Also don't use the anim name as a string like "MaidWalk", use instead MaidWalk.name, will guarantee that you aren't misspelling it

            //AnimationSetMaid.anim.Play("MaidWalk");

            //  Use this method
            AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidWalk.name, 0.2F, PlayMode.StopAll);

            // AI.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            waypointInd++;
            if (waypointInd >= waypoints.Length)
            {
                waypointInd = 0;

            }
        }


        // direction = waypoints[waypointInd].transform.position - transform.position;

        //else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        //{
        //    waypointInd = Random.Range(0, waypoints.Length);
        //}
        else
        {
            //    AI.Move(Vector3.zero, false, false);
        }
        Debug.Log("going to checkpoint");


        //AI.rotation = Quaternion.Slerp(AI.rotation, Quaternion.LookRotation(target.position + AI.position), rotSpeed * Time.deltaTime);



    }



    void Hurt()
    {



        //character.Move(agent.desiredVelocity, false, false);




        if (HurtPlayer == false)
        {
            AnimationSetMaid.anim.clip = AnimationSetMaid.MaidHurt;
            AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidHurt.name, 0.2F, PlayMode.StopAll);
            timer += Time.deltaTime;
            rend.sharedMaterial = material[2];
            agent.SetDestination(this.transform.position);
            //character.Move(Vector3.zero, false, false);
            Debug.Log("Hurting plYER");
            Debug.Log(hurttimer);
            Vector3 LookPos = target.transform.position;
            LookPos.y = transform.position.y;
            transform.LookAt(LookPos);

            if (timer >= Killingplayer)
            {
                Destroy(target);
            }
            
            if (timer >= hurttimer)
            {
                state = Maid_AI.State.PATROL;
                timer = 0;
                // Destroy(target);
            }
        }
        //else if (HurtPlayer == true)
        //{
        //    if (timer >= Killingplayer)
        //    {
        //        Destroy(target);
        //    }
        //    AnimationSetMaid.anim.clip = AnimationSetMaid.MaidHurt;
        //    AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidHurt.name, 0.2F, PlayMode.StopAll);

        //}


    }




    void Investigate()
    {
        //if (Workfunct == false)
        // return;


        Debug.Log("investigating area");
        Debug.Log(investigateWait);

        AnimationSetMaid.anim.clip = AnimationSetMaid.MaidInvestigate;
        AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidInvestigate.name, 0.2F, PlayMode.StopAll);
        //investigateSpot = target.transform.position;
       if (Seen == true)
       {
            rend.sharedMaterial = material[1];
            Debug.Log("saw something");
            agent.speed = chasespeed;
            agent.SetDestination(target.transform.position);
            //  agent.SetDestination(this.transform.position);
            timer += Time.deltaTime;
            
        }
        if (Seen == false)
        {
            rend.sharedMaterial = material[1];
            Debug.Log("saw something");
            agent.speed = chasespeed;
            agent.SetDestination(target.transform.position);
            //  agent.SetDestination(this.transform.position);
            timer += Time.deltaTime;

        }



        if (timer >= investigateWait)
        {
            Debug.Log("FUCK");
            state = Maid_AI.State.PATROL;
            timer = 0;
        }




    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            state = Maid_AI.State.INVESTIGATE;
            investigateSpot = coll.gameObject.transform.position;
            
        }

    }


    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * CanSee, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * CanSee, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * CanSee, Color.green);


        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * CannotSee, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * CannotSee, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * CannotSee, Color.red);




        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CannotSee))
        {
            

            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Investigating");
                state = Maid_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                Debug.Log("FUCK bsdhvdshbvjkdb");
                //if (timer >= investigateWait)
                //{
                //    state = Maid_AI.State.PATROL;
                //    timer = 0;
                //}
                state = Maid_AI.State.INVESTIGATE;
                Seen = false;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Maid_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                //if (timer >= investigateWait)
                //{
                //    state = Maid_AI.State.PATROL;
                //    timer = 0;
                //}
                Seen = false;
                state = Maid_AI.State.INVESTIGATE;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Maid_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                //if (timer >= investigateWait)
                //{
                //    state = Maid_AI.State.PATROL;
                //    timer = 0;
                //}
                state = Maid_AI.State.INVESTIGATE;
                Seen = false;
            }
        }



        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CanSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Maid_AI.State.HURT;
                target = hit.collider.gameObject;
                HurtPlayer = true;
            }
            else
            {

                HurtPlayer = false;


                state = Maid_AI.State.HURT;
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CanSee))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = Maid_AI.State.HURT;
                    target = hit.collider.gameObject;
                    HurtPlayer = true;
                }
                else
                {
                    HurtPlayer = false;


                    state = Maid_AI.State.HURT;



                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CanSee))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = Maid_AI.State.HURT;
                    target = hit.collider.gameObject;
                    HurtPlayer = true;
                }
                else
                {
                    HurtPlayer = false;


                    state = Maid_AI.State.HURT;



                }
            }







        }


    }
}



