using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollision : MonoBehaviour {

    public bool HitTarget = false;
    public bool HitTarget2 = false;
    public float possessionTimer = 0;
    public bool HitRune = false;
    public bool HitLastRune = false;
    public GameObject keyCollected;
    public GameObject AIHit;
    public GameObject runeImage;
    public GameObject playerHit;


    // Use this for initialization
    void Start () {

        runeImage = GameObject.Find("RuneImage");
        playerHit = GameObject.Find("Player_Character");
        MagnetCollision magnetScript = keyCollected.GetComponent<MagnetCollision>();

    }
	
	// Update is called once per frame
	void Update () {
        RuneInventory runesInventory = runeImage.GetComponent<RuneInventory>();
    }

    void OnTriggerStay(Collider other)
    {


        ImmRune immuneRune = playerHit.GetComponent<ImmRune>();

        if (other.gameObject.tag == "AI" && Input.GetMouseButtonDown(0) && immuneRune.timer <=0)
        {
            AIHit = other.gameObject;
            HitTarget = true;
        }

        if (other.gameObject.tag == "AI" && Input.GetMouseButtonDown(0) && possessionTimer <= 0)
        {
            AIHit = other.gameObject;
            HitTarget2 = true;
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


        ImmRune immuneRune = playerHit.GetComponent<ImmRune>();
        if (other.gameObject.tag == "AI" && immuneRune.immobilised == false)
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
