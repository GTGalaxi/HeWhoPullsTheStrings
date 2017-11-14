using System.Collections;
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
    public bool reverse = false;
    
    public Material[] material;
    Renderer rend;
    public bool Seen = false;
    private bool OutofRange = false;
    public bool HurtPlayer = false;
    
    void Start()
    {
        AnimationSetMaid.anim = GetComponent<Animation>();
        
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

        agent = GetComponent<NavMeshAgent>();
        
        agent.updatePosition = true;
        agent.updateRotation = true;
        
        state = Maid_AI.State.PATROL;

        alive = true;
        StartCoroutine(FSM());

        heightMultiplier = 1.36f;
    }

    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
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
        rend.sharedMaterial = material[0];
        agent.speed = patrolSpeed;
        
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) > 5)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
            AnimationSetMaid.anim.clip = AnimationSetMaid.MaidWalk;
            AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidWalk.name, 0.2F, PlayMode.StopAll);
        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {
            if (reverse == false)
            {
                waypointInd++;
                if (waypointInd >= waypoints.Length)
                {
                    reverse = true;
                }
            }
            if (reverse == true)
            {
                waypointInd--;
                if (waypointInd == 0)
                {
                    reverse = false;
                }
            }
        }
    }

    void Hurt()
    {
        if (HurtPlayer == false)
        {
            AnimationSetMaid.anim.clip = AnimationSetMaid.MaidHurt;
            AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidHurt.name, 0.2F, PlayMode.StopAll);
            timer += Time.deltaTime;
            rend.sharedMaterial = material[2];
            agent.SetDestination(this.transform.position);
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
            }
        }
    }




    void Investigate()
    {
        Debug.Log(timer);

        AnimationSetMaid.anim.clip = AnimationSetMaid.MaidInvestigate;
        AnimationSetMaid.anim.CrossFade(AnimationSetMaid.MaidInvestigate.name, 0.2F, PlayMode.StopAll);

       if (Seen == true)
       {
            rend.sharedMaterial = material[1];
            agent.speed = chasespeed;
            agent.SetDestination(target.transform.position);
            timer = 0;
        }

        if (Seen == false)
        {
            rend.sharedMaterial = material[0];
            agent.speed = chasespeed;
            agent.SetDestination(this.transform.position);
            timer += Time.deltaTime;
        }

        if (timer >= investigateWait)
        {
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
        
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CannotSee) || Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CannotSee) || Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CannotSee))
        {
            if (hit.collider.gameObject == target)
            {
                Seen = true;
                state = Maid_AI.State.INVESTIGATE;
                target = hit.collider.gameObject;
            }
            else
            {
                Seen = false;
                state = Maid_AI.State.PATROL;
            }
        }

        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, CanSee) || Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, CanSee) || Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, CanSee))
        {
            if (hit.collider.gameObject == target)
            {
                state = Maid_AI.State.HURT;
                target = hit.collider.gameObject;
                HurtPlayer = true;
            }
            else
            {
                HurtPlayer = false;
                state = Maid_AI.State.INVESTIGATE;
            }
        }


    }
}