using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollision : MonoBehaviour {

    public bool HitTarget = false;
    public GameObject keyCollected;

    // Use this for initialization
    void Start () {

        
        MagnetCollision magnetScript = keyCollected.GetComponent<MagnetCollision>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Target")
        {

            HitTarget = true;


        }
    }

    void OnTriggerExit(Collider other)
    {


        if (other.gameObject.tag == "Target")
        {

            HitTarget = false;

        }
    }

}
