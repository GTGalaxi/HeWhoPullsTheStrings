using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class NewAIStill : MonoBehaviour {




	public Slider healthbar;
	public float health = 100.0f;

    public GameObject sword;


    public Transform player;
	Animator anim;
	//public Transform head;

	public float KnockBack = 0f;
	public float Following = 10.0f;
	public float Attacking = 5.0f;
	public float Degreesight = 30;
	public float SpeedToTarget;
	public float RotationToTarget;
	//bool chasing = false;

	//string state = "patrol";
	//public GameObject[] waypoints;
	//int currentWP = 0;
	public float rotSpeed = 0.2f;
	public float speed = 1.5f;
	//public float accuarcyWP = 5.0f;

	public AudioSource sounds;
	public AudioClip[] hitSounds = new AudioClip[5];
	bool targeted = false;





	// Use this for initialization
	void Start () 
	{
        sword.tag = "Untagged";

        anim = GetComponent<Animator> ();
		sounds = gameObject.GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag("Player").transform; //automatically find the player character
	}

	// Update is called once per frame
	void Update () 
	{
		SearchandFollow ();
		healthbar.value = health;
		if (health <= 0)
		{
			Destroy(gameObject, 2);
		}
	}

	void Pathfinding(){




	}



	void SearchandFollow()
	{


		if (Vector3.Distance(player.position, this.transform.position) < Following)
		{
			targeted = true;
		}




		if (healthbar.value <= 0) return;



        if (targeted == true)
        {




            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            //float angle = Vector3.Angle(direction, this.head.up);
            //state = "chasing";
            //chasing = true;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), RotationToTarget);

            //anim.SetBool ("Idle", false);

            if (direction.magnitude > Attacking)
            {

                this.transform.Translate(0, 0, SpeedToTarget);
                anim.SetBool("Walking", true);
                anim.SetBool("Attack", false);
                sword.tag = "Untagged";


            }
            else
            {

                anim.SetBool("Attack", true);
                anim.SetBool("Walking", false);
                sword.tag = "AI";

            }

        }
        else
        {
            //anim.SetBool ("Idle", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Attack", false);
            //chasing = false;
            //state = "patrol";
        }



	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerLightAttack") {
			health -= 40;
			sounds.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
			this.transform.Translate(0, 0, KnockBack);

		}
		if (other.gameObject.tag == "PlayerHeavyAttack")
		{
			health -= 70;
			sounds.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
			this.transform.Translate(0, 0, KnockBack * 1.5f);
		}

		if (health <= 0) {
			anim.SetTrigger ("Death");
			anim.SetBool ("Walking", false);
			targeted = false;
            sword.tag = "Untagged";
        }
        else {
			anim.SetBool ("Walking", true);
		}
		Debug.Log ("Hit");


	}



	/*void OnTriggerExit(Collider dead)
	{
		if (health <= 0 ){
			Destroy (gameObject, 2);
		}
	}*/

}