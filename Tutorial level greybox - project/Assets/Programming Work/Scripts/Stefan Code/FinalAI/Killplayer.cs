using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killplayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject player;

   
    




     void OnTriggerEnter(Collider other)
     {
        if (other.gameObject.tag == "Player")
        {
            Destroy(player);
        }
    }
}
