using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Script_General_AI : MonoBehaviour
{


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
    private int waypointInd;
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



    public float timerRune = 5;
    public bool startruneTimer = false;
    public bool immRunefinished = false;
    private bool Workfunct = true;

    // changes behaviour of ai depending on stages
    public Material[] material;
    Renderer rend;
    // checks if player hase been seen
    private bool Seen = false;
    private bool OutofRange = false;
    private bool HurtPlayer = false;

    // Use this for initialization
    void Start()
    {

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
        state = Script_General_AI.State.PATROL;

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



        if (timerRune <= 0)
        {
            immRunefinished = true;
            startruneTimer = false;
        }
        else if (timerRune > 0)
        {
            timerRune = timerRune - (1 * Time.deltaTime);
            // print(timerRune);
        }
        if (immRunefinished == true)
        {
            timerRune = 5;
            startruneTimer = false;

        }
        else
        {
            immRunefinished = false;
        }

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


        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            //Vector3 LookPos = waypoints[waypointInd].transform.position;
            //LookPos.y = transform.position.y;
            //transform.LookAt(LookPos);
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookPos), rotSpeed * Time.deltaTime);
            agent.SetDestination(waypoints[waypointInd].transform.position);


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
        if (Workfunct == false)
            return;


        //character.Move(agent.desiredVelocity, false, false);

        timer += Time.deltaTime;
        rend.sharedMaterial = material[2];
        agent.SetDestination(this.transform.position);
        //character.Move(Vector3.zero, false, false);
        Debug.Log("Hurting plYER");
        Debug.Log(hurttimer);
        Vector3 LookPos = target.transform.position;
        LookPos.y = transform.position.y;
        transform.LookAt(LookPos);

        if (timer >= hurttimer)
        {
            state = Script_General_AI.State.PATROL;
            timer = 0;
            Destroy(target);
        }

    }




    void Investigate()
    {
        //if (Workfunct == false)
        // return;


        Debug.Log("investigating area");
        Debug.Log(investigateWait);


        if (Seen == true)
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
            state = Script_General_AI.State.PATROL;
            timer = 0;
        }




    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            state = Script_General_AI.State.INVESTIGATE;
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
                state = Script_General_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
                Seen = true;
            }
            else
            {
                Debug.Log("FUCK bsdhvdshbvjkdb");
                if (timer >= investigateWait)
                {
                    state = Script_General_AI.State.PATROL;
                    timer = 0;
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_General_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_General_AI.State.PATROL;
                    timer = 0;
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_General_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_General_AI.State.PATROL;
                    timer = 0;
                }
            }
        }



        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CanSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_General_AI.State.HURT;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_General_AI.State.PATROL;
                    timer = 0;
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CanSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_General_AI.State.HURT;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_General_AI.State.PATROL;
                    timer = 0;
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CanSee))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_General_AI.State.HURT;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_General_AI.State.PATROL;
                    timer = 0;
                }
            }
        }







    }


}




