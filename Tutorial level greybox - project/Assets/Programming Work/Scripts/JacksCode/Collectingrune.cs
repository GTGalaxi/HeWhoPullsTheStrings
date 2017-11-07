using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectingrune : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        GameObject hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();

        if (hitBoxScript.HitRune == true)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                Application.LoadLevel(Application.loadedLevel +1);
            }

        }
    }
}
