using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAISight : MonoBehaviour {



    public GameObject HurtScanner;
    public GameObject InvestigateScanner;
    public GameObject target;
    public NavMeshAgent agent;
    public GameObject[] waypoints;
    // changes behaviour of ai depending on stages
    public Material[] material;
    Renderer rend;

    public enum State
    {
        PATROL,
        INVESTIGATE,
        HURT
        
    }
    public State state;



    //For waypoints
    private int waypointInd;
    //patrolling
    public float patrolSpeed = 0.5f;
    public float chasespeed = 1f;

   
    // is the ai alive?
    private bool alive;


    // Use this for initialization
    void Start ()
    {
        //starts with a colour of choice
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];


        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;

        
        waypointInd = Random.Range(0, waypoints.Length);
        //statrting state
        state = TestAISight.State.PATROL;
        //starts ai being alive
        alive = true;
        // Start FSM
        StartCoroutine(FSM());

      



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
        // makes sure the colour starts of as whatever it is as element 0
        rend.sharedMaterial = material[0];
        // produces a message saying its going to checkpoint
        Debug.Log("going to checkpoint");
        // navmesh agent speed is equal to whatever is set in the inspector.
        agent.speed = patrolSpeed;
        // AI Going towards checkpoints from whatever position they are in.
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            waypointInd = Random.Range(0, waypoints.Length);
        }
       
    }




    void Investigate() { }
    void Hurt() { }

    // Update is called once per frame
    void Update () {
		
	}





    















































}
