using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteract : MonoBehaviour {
    [SerializeField]
    public static bool ButtonPressed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == "Button")
        {
            ButtonPressed = true;
        }
    }

}
