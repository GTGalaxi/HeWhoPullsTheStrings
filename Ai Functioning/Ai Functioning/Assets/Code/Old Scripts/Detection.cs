using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Detection : MonoBehaviour {



	Animator anim;
	public Slider healthbar;
	public float health = 100.0f;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.value = health;

	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerAttack") {
			health -= 20;
		}

		if (health <= 0) {
			anim.SetBool ("Dead", true);
			anim.SetBool ("Walking", false);
		} else {
			anim.SetBool ("Dead", false);
		}
		Debug.Log ("Hit");
	}



	void OnTriggerExit(Collider dead)
	{
		if (health <= 0 ){
			Destroy (gameObject, 2);
		}
	}























}
