using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



[System.Serializable]
public class AnimationSetGeneralStill
{
    public Animation anim;
    public AnimationClip GeneralIdle;
    public AnimationClip GeneralInvestigate;
    public AnimationClip GeneralHurt;
    public AnimationClip GeneralWalk;
}










public class General_AI_Still : MonoBehaviour
{





    public AnimationSetGeneralStill AnimationSetGeneralStill = new AnimationSetGeneralStill();




    public NavMeshAgent agent;
    //   public GameObject AI;

    public enum State
    {
        IDLE,
        HURT,
        INVESTIGATE


    }

    public State state;
    private bool alive;

    //Variable for IDLEling
    public GameObject[] waypoints;
    private int waypointInd;
    public float IDLESpeed = 0.5f;
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

        AnimationSetGeneralStill.anim = GetComponent<Animation>();


        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        // character.GetComponent<ImmRune>().KnockOutRune();
        //ImmRune KO = gameObject.GetComponent<ImmRune>();

        agent = GetComponent<NavMeshAgent>();


        agent.updatePosition = true;

        agent.updateRotation = true;

        //waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        waypointInd = Random.Range(0, waypoints.Length);
        state = General_AI_Still.State.IDLE;

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

                case State.IDLE:
                    Idle();
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

    void Idle()
    {



        rend.sharedMaterial = material[0];

        Debug.Log("going to checkpoint");
        agent.speed = IDLESpeed;
        AnimationSetGeneralStill.anim.CrossFade(AnimationSetGeneralStill.GeneralIdle.name, 0.2F, PlayMode.StopAll);
        AnimationSetGeneralStill.anim.clip = AnimationSetGeneralStill.GeneralIdle;

        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            //Vector3 LookPos = waypoints[waypointInd].transform.position;
            //LookPos.y = transform.position.y;
            //transform.LookAt(LookPos);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookPos), rotSpeed * Time.deltaTime);
            agent.SetDestination(waypoints[waypointInd].transform.position);



            AnimationSetGeneralStill.anim.clip = AnimationSetGeneralStill.GeneralWalk;

            //  Don't use anim.Play as you can't blend anims
            //  Also don't use the anim name as a string like "GeneralWalk", use instead GeneralWalk.name, will guarantee that you aren't misspelling it

            //AnimationSetGeneralStill.anim.Play("GeneralWalk");

            //  Use this method
            AnimationSetGeneralStill.anim.CrossFade(AnimationSetGeneralStill.GeneralWalk.name, 0.2F, PlayMode.StopAll);

            // AI.Move(agent.desiredVelocity, false, false);
        }

        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            waypointInd = Random.Range(0, waypoints.Length);
        }
        else
        {
            //AI.Move(Vector3.zero, false, false);
        }
        Debug.Log("going to checkpoint");


        //AI.rotation = Quaternion.Slerp(AI.rotation, Quaternion.LookRotation(target.position + AI.position), rotSpeed * Time.deltaTime);



    }



    void Hurt()
    {



        //character.Move(agent.desiredVelocity, false, false);




        if (HurtPlayer == false)
        {
            AnimationSetGeneralStill.anim.clip = AnimationSetGeneralStill.GeneralHurt;
            AnimationSetGeneralStill.anim.CrossFade(AnimationSetGeneralStill.GeneralHurt.name, 0.2F, PlayMode.StopAll);
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
                state = General_AI_Still.State.IDLE;
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
        //    AnimationSetGeneralStill.anim.clip = AnimationSetGeneralStill.GeneralHurt;
        //    AnimationSetGeneralStill.anim.CrossFade(AnimationSetGeneralStill.GeneralHurt.name, 0.2F, PlayMode.StopAll);

        //}


    }




    void Investigate()
    {
        //if (Workfunct == false)
        // return;


        Debug.Log("investigating area");
        Debug.Log(investigateWait);

        AnimationSetGeneralStill.anim.clip = AnimationSetGeneralStill.GeneralInvestigate;
        AnimationSetGeneralStill.anim.CrossFade(AnimationSetGeneralStill.GeneralInvestigate.name, 0.2F, PlayMode.StopAll);
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
            state = General_AI_Still.State.IDLE;
            timer = 0;
        }




    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            state = General_AI_Still.State.INVESTIGATE;
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
                state = General_AI_Still.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                Debug.Log("FUCK bsdhvdshbvjkdb");
                //if (timer >= investigateWait)
                //{
                //    state = General_AI_Still.State.IDLE;
                //    timer = 0;
                //}
                state = General_AI_Still.State.INVESTIGATE;
                Seen = false;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = General_AI_Still.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                //if (timer >= investigateWait)
                //{
                //    state = General_AI_Still.State.IDLE;
                //    timer = 0;
                //}
                Seen = false;
                state = General_AI_Still.State.INVESTIGATE;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = General_AI_Still.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                //if (timer >= investigateWait)
                //{
                //    state = General_AI_Still.State.IDLE;
                //    timer = 0;
                //}
                state = General_AI_Still.State.INVESTIGATE;
                Seen = false;
            }
        }



        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CanSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = General_AI_Still.State.HURT;
                target = hit.collider.gameObject;
                HurtPlayer = true;
            }
            else
            {

                HurtPlayer = false;


                state = General_AI_Still.State.HURT;
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CanSee))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = General_AI_Still.State.HURT;
                    target = hit.collider.gameObject;
                    HurtPlayer = true;
                }
                else
                {
                    HurtPlayer = false;


                    state = General_AI_Still.State.HURT;



                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CanSee))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = General_AI_Still.State.HURT;
                    target = hit.collider.gameObject;
                    HurtPlayer = true;
                }
                else
                {
                    HurtPlayer = false;


                    state = General_AI_Still.State.HURT;



                }
            }







        }


    }
}

