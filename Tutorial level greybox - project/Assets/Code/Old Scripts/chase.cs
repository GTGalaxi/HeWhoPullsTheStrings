using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chase : MonoBehaviour {







	public Transform player;
	 Animator anim;
	public Transform head;


	public float Following = 10.0f;
	public float Attacking = 5.0f;
	public float Degreesight = 30;


	//bool chasing = false;

	string state = "patrol";
	public GameObject[] waypoints;
	int currentWP = 0;
	public float rotSpeed = 0.2f;
	public float speed = 1.5f;
	public float accuarcyWP = 5.0f;







	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		SearchandFollow ();

	
	}

	void Pathfinding(){
		



	}



	void SearchandFollow()
	{
		
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		float angle = Vector3.Angle (direction, this.head.up);





		if (state == "patrol" && waypoints.Length > 0) 
		{
			anim.SetBool ("Idle", false);
			anim.SetBool ("Walking", true);
			if (Vector3.Distance (waypoints [currentWP].transform.position, transform.position) < accuarcyWP)
			{


				currentWP = Random.Range (0, waypoints.Length);

				//one after another 
				/*currentWP++;
				if (currentWP >= waypoints.Length) 
				{
					currentWP = 0;
				}*/
			}
			direction  = waypoints [currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, Time.deltaTime * speed);
		}


		if (Vector3.Distance (player.position, this.transform.position) <  Following && (angle <  Degreesight || state == "chasing")) 
		{
			
			state = "chasing";
			//chasing = true;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			//anim.SetBool ("Idle", false);
		
			if (direction.magnitude > Attacking)
				{
				
				this.transform.Translate (0, 0, Time.deltaTime * speed);
				anim.SetBool ("Walking", true);
				anim.SetBool ("Attack", false);


				} 
				else 
					{
			
						anim.SetBool ("Attack", true);
						anim.SetBool ("Walking", false);
				
					}
		
		
		} 
		else 
		{
			//anim.SetBool ("Idle", true);
			anim.SetBool ("Walking", true);
			anim.SetBool ("Attack", false);
			//chasing = false;
			state = "patrol";
		}



	}


}
