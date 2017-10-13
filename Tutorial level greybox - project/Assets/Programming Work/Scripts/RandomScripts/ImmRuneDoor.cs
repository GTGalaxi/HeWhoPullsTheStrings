using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmRuneDoor : MonoBehaviour {


    public bool doorReady = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {

            doorReady = true;

        }
	}


    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E) && doorReady == true)
        {

            Application.LoadLevel(2);

        }
    }


}
