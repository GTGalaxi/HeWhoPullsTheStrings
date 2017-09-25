using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Script_AI_Test : MonoBehaviour
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
    public float rotSpeed = 0.2f;


    //Variables for Chasing
    public GameObject target;
    //  public float chaseSpeed = 1f;
    //public Transform target;
    //private Transform AI;
    public float hurttimer = 5;

    //Variables for investigating
    private Vector3 investigateSpot;
    private float timer = 0;
    public float investigateWait = 10;

    //Variables for sight
    public float heightMultiplier;
    public float sightDist = 10;

    public float timerRune = 5;
    public bool startruneTimer = false;
    public bool immRunefinished = false;
    private bool Workfunct = true;

    // Use this for initialization
    void Start()
    {
        // character.GetComponent<ImmRune>().KnockOutRune();
        //ImmRune KO = gameObject.GetComponent<ImmRune>();

        agent = GetComponent<NavMeshAgent>();
        

        agent.updatePosition = true;
        
        agent.updateRotation = true; 

        //waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        waypointInd = Random.Range(0, waypoints.Length);
        state = Script_AI_Test.State.PATROL;

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

        agent.SetDestination(this.transform.position);
        //character.Move(Vector3.zero, false, false);
        Debug.Log("Hurting plYER");
        Vector3 LookPos = target.transform.position;
        LookPos.y = transform.position.y;
        transform.LookAt(LookPos);
        if (timer >= hurttimer)
        {
            state = Script_AI_Test.State.PATROL;
            timer = 0;
        }
    }

    void Investigate()
    {
        //if (Workfunct == false)
        // return;

        //agent.speed = chaseSpeed;
        //agent.SetDestination(target.transform.position);

        timer += Time.deltaTime;

        agent.SetDestination(this.transform.position);
        // character.Move(Vector3.zero, false, false);
        Debug.Log("investigating area");
        Vector3 LookPos = target.transform.position;
        LookPos.y = transform.position.y;
        transform.LookAt(LookPos);
        if (timer >= investigateWait)
        {
            state = Script_AI_Test.State.PATROL;
            timer = 0;
        }
        //else { FixedUpdate(); }
        
        




    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            state = Script_AI_Test.State.INVESTIGATE;
            investigateSpot = coll.gameObject.transform.position;
        }

    }


    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_AI_Test.State.HURT;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_AI_Test.State.PATROL;
                    timer = 0;
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_AI_Test.State.HURT;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_AI_Test.State.PATROL;
                    timer = 0;
                }
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                state = Script_AI_Test.State.HURT;
                target = hit.collider.gameObject;
            }
            else
            {
                if (timer >= investigateWait)
                {
                    state = Script_AI_Test.State.PATROL;
                    timer = 0;
                }
            }
        }
    }



































}
