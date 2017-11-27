using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankAppearing : MonoBehaviour {

    public bool buttonPressed2 = false;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (AIInteract.ButtonPressed == true)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        buttonPressed2 = AIInteract.ButtonPressed;


	}
}
