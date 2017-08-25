using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class TestScriptIII : MonoBehaviour
    {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE


        }

        public State state;
        private bool alive;

        //Variable for patrolling
        public GameObject[] waypoints;
        private int waypointInd;
        public float patrolSpeed = 0.5f;



        //Variables for Chasing

        public float chaseSpeed = 1f;
        public GameObject target;

        //Camera sight variables
        public GameObject player;
        public Collider playercoll;
        public Camera myCam;
        private Plane[] planes;

        

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
            waypointInd = Random.Range(0, waypoints.Length);

            playercoll = player.GetComponent<Collider>();
            state = TestScriptIII.State.PATROL;

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
                    case State.CHASE:
                        Chase();
                        break;
                    

                }
                yield return null;
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {

                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
            {
                waypointInd = Random.Range(0, waypoints.Length);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);

        }


        void Update()
        {
            planes = GeometryUtility.CalculateFrustumPlanes(myCam);
            if (GeometryUtility.TestPlanesAABB(planes, playercoll.bounds))
            {
                Debug.Log("player sighted");
                CheckForPlayer();
            }
            else
            { }
        }


        void CheckForPlayer()
        {
            RaycastHit hit;
            Debug.DrawRay(myCam.transform.position, transform.forward * 10, Color.green);
            if (Physics.Raycast(myCam.transform.position, transform.forward, out hit, 10))
            {
                state = TestScriptIII.State.CHASE;
                target = hit.collider.gameObject;
            }
        }





    }


}