using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollision : MonoBehaviour {

    public bool HitTarget = false;
    public bool HitRune = false;
    public bool HitLastRune = false;
    public GameObject keyCollected;
    public GameObject AIHit;

    // Use this for initialization
    void Start () {

        
        MagnetCollision magnetScript = keyCollected.GetComponent<MagnetCollision>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "AI")
        {
            AIHit = other.gameObject;
            HitTarget = true;


        }

        if (other.gameObject.tag == "Rune")
        {

            HitRune = true;


        }

        if (other.gameObject.tag == "LastRune")
        {

            HitLastRune = true;


        }

    }

    void OnTriggerExit(Collider other)
    {


        if (other.gameObject.tag == "AI")
        {
            AIHit = null;
            HitTarget = false;

        }

        if (other.gameObject.tag == "Rune")
        {

            HitRune = false;


        }

        if (other.gameObject.tag == "LastRune")
        {

            HitLastRune = false;


        }

    }

}
