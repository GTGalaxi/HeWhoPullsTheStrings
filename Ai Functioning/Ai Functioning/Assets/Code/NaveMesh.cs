using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaveMesh : MonoBehaviour {


    public Transform[] WayPoints;

    private int destPoint = 0;
    private NavMeshAgent agent;



    // Use this for initialization
    void Start () {


         agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();

    }
	
	



    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (WayPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = WayPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % WayPoints.Length;
    }




    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

    }










}
