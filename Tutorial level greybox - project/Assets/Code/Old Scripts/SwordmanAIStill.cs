
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]


public class  SwordmanAIStill : MonoBehaviour {
	public Transform player;
	static Animator anim;
	public float Following = 40.0f;
	public float Attacking = 5.0f;
    public float SpeedToTarget;
    public float RotationToTarget;
    private bool targeted = false;

    public GameObject sword;


	public Slider healthbar;
    public float health = 100.0f;
    public AudioSource sounds;
    public AudioClip[] hitSounds = new AudioClip[5];
    private float hitstun;


	void Start () 
	{
		anim = GetComponent<Animator> ();
        sounds = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //automatically find the player character
        sword.tag = "Untagged";
    }

	void Update () 
	{
		SearchandFollow ();
		healthbar.value = health;
//        Debug.Log(health);
        // hitstun += 1 * Time.deltaTime;
        if (health <= 0.1f)
        {
            Destroy(gameObject, 2);
            //  anim.SetTrigger("Death");

        }
    }




    // this is where health, searching for player and attacking player happens. as well playing animation.
	void SearchandFollow()
	{
		if (healthbar.value <= 0) return;

		if (Vector3.Distance (player.position, this.transform.position) < Following && health > 0)
        {
            targeted = true;
        }


        if (targeted == true)
        {
			Vector3 direction = player.position - this.transform.position;
			direction.y = 0;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), RotationToTarget);

			anim.SetBool ("Idle", false);

			if (direction.magnitude > Attacking)
			{

				this.transform.Translate (0, 0, SpeedToTarget);
				anim.SetBool ("Walking", true);
				anim.SetBool ("Attack", false);
                sword.tag = "Untagged";


            }
            else 
			{
                sword.tag = "EnemySword";

                anim.SetBool ("Attack", true);
				anim.SetBool ("Walking", false);

			}
		} 




		else 
		{
			anim.SetBool ("Idle", true);
			anim.SetBool ("Walking", false);
			anim.SetBool ("Attack", false);

		}



	}

	void OnTriggerEnter(Collider other)
	{

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            targeted = false;

        }
        
        Debug.Log("Hit");

//        if (hitstun >= 0.3f)
  //      {
            if (other.gameObject.tag == "PlayerLightAttack")
            {
                health -= 20;
                sounds.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            }
            if (other.gameObject.tag == "PlayerHeavyAttack")
            {
                health -= 35;
                sounds.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            }
       //     hitstun = 0.0f;
    //    }


	}



	/*void OnTriggerExit(Collider dead)
	{
		if (health <= 0.1f ){
			Destroy (gameObject, 2);
          //  anim.SetTrigger("Death");

        }
	}*/
}
