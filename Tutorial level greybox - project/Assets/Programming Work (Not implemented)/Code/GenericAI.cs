using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class GenericAI : MonoBehaviour
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

      //  public float chaseSpeed = 1f;
        public GameObject target;
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
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            //waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
            waypointInd = Random.Range(0, waypoints.Length);
            state = GenericAI.State.PATROL;

            alive = true;
            // Start FSM
            StartCoroutine(FSM());

            heightMultiplier = 1.36f;



        }

        void Update()
        {



            if (timerRune <= 0)
            {
                immRunefinished = true;
                startruneTimer = false;
            }
            else if(timerRune > 0)
            {
                timerRune = timerRune - (1 * Time.deltaTime);
                print(timerRune);
            }
            if(immRunefinished == true)
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

            IMrune();





        }


        void IMrune()
        {
            if (Input.GetKey(KeyCode.E) && startruneTimer == false)
            {
                agent.GetComponent<NavMeshAgent>().enabled = false;
                startruneTimer = true;
                //Workfunct = false;
                immRunefinished = false;
            }
           
            if(immRunefinished == true)
            {
                Workfunct = true;
                agent.GetComponent<NavMeshAgent>().enabled = true;
                
            }


        }





        void Hurt()
        {
            if (Workfunct == false)
               return;
                
            // agent.speed = chaseSpeed;
            // agent.SetDestination(target.transform.position);
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
                state = GenericAI.State.PATROL;
                timer = 0;
            }
        }

        void Investigate()
        {
            //if (Workfunct == false)
               // return;

            timer += Time.deltaTime;
            
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            Debug.Log("investigating area");
            Vector3 LookPos = target.transform.position;
            LookPos.y = transform.position.y;
            transform.LookAt(LookPos);
            if (timer >= investigateWait)
            {
                state = GenericAI.State.PATROL;
                timer = 0;
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                state = GenericAI.State.INVESTIGATE;
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
                    state = GenericAI.State.HURT;
                    target = hit.collider.gameObject;
                }
                else
                {
                    if (timer >= investigateWait)
                    {
                        state = GenericAI.State.PATROL;
                        timer = 0;
                    }
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = GenericAI.State.HURT;
                    target = hit.collider.gameObject;
                }
                else
                {
                    if (timer >= investigateWait)
                    {
                        state = GenericAI.State.PATROL;
                        timer = 0;
                    }
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = GenericAI.State.HURT;
                    target = hit.collider.gameObject;
                }
                else
                {
                    if (timer >= investigateWait)
                    {
                        state = GenericAI.State.PATROL;
                        timer = 0;
                    }
                }
            }
        }



    }


}