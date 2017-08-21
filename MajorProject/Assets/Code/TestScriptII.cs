using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class TestScriptII : MonoBehaviour
    {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

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



        //Variables for Chasing

        public float chaseSpeed = 1f;
        public GameObject target;
        public float hurttimer = 5;

        //Variables for investigating
        private Vector3 investigateSpot;
        private float timer = 0;
        public float investigateWait = 10;

        //Variables for sight
        public float heightMultiplier;
        public float sightDist = 10;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            //waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
            waypointInd = Random.Range(0, waypoints.Length);
            state = TestScriptII.State.PATROL;

            alive = true;
            // Start FSM
            StartCoroutine(FSM());

            heightMultiplier = 1.36f;



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
            Debug.Log("going to checkpoint");
        }

        void Hurt()
        {
            // agent.speed = chaseSpeed;
            // agent.SetDestination(target.transform.position);
            //character.Move(agent.desiredVelocity, false, false);
            
            timer += Time.deltaTime;

            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            Debug.Log("Hurting player");
            transform.LookAt(investigateSpot);
            if (timer >= hurttimer)
            {
                state = TestScriptII.State.PATROL;
                timer = 0;
            }
        }

        void Investigate()
        {
            timer += Time.deltaTime;
            
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            Debug.Log("investigating area");
            transform.LookAt(investigateSpot);
            if (timer >= investigateWait)
            {
                state = TestScriptII.State.PATROL;
                timer = 0;
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                state = TestScriptII.State.INVESTIGATE;
				Vector3 InvestigatePos = coll.gameObject.transform.position;
				InvestigatePos.y = this.gameObject.transform.position.y;
                investigateSpot = InvestigatePos;
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
                    state = TestScriptII.State.HURT;
                    target = hit.collider.gameObject;
                }
                else
                {
                    if (timer >= investigateWait)
                    {
                        state = TestScriptII.State.PATROL;
                        timer = 0;
                    }
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = TestScriptII.State.HURT;
                    target = hit.collider.gameObject;
                }
                else
                {
                    if (timer >= investigateWait)
                    {
                        state = TestScriptII.State.PATROL;
                        timer = 0;
                    }
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = TestScriptII.State.HURT;
                    target = hit.collider.gameObject;
                }
                else
                {
                    if (timer >= investigateWait)
                    {
                        state = TestScriptII.State.PATROL;
                        timer = 0;
                    }
                }
            }
        }



    }


}