using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {






    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}


    void OnTriggerEnter(Collider coll)
    {

        if (coll.tag == "Player")
        {
            animator.SetBool("open", true);

        }
       
    }

     void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            animator.SetBool("open", false);

        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
