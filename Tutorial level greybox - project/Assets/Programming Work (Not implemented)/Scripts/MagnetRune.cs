using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetRune : MonoBehaviour {
    
    //Raycasting
    public float heightMultiplier;
    public float sightDist = 10;
    Renderer objRenderer;
    public Material newMtrl;
    public Material oldMtrl;
    public GameObject obj;

    public GameObject magnetRune;
    public bool collectMRune = true;
    public bool collectKey = true;
    public int timeSinceCollision;
    public bool seeMrune = true;

    public Text visionRune;
    public Text magnetText;


    // Use this for initialization
    void Start () {

        magnetText.enabled = false;


    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);

        if (Input.GetMouseButton(0) && seeMrune == false)
        {

            magnetRune.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            seeMrune = true;
            visionRune.enabled = false;

        }





    }

    void OnCollisionStay(Collision other)
    {



    }

    void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Magnetised")
        {
            collectKey = true;
            print("Collected Key");
            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == "Door" && Input.GetKey(KeyCode.E))
        {
            if(collectKey == true)
            {
                print("New Scene");
                Application.LoadLevel("Immobilisation rune");
            }
        }

        if (other.gameObject.tag == "magnetRune" && Input.GetKey(KeyCode.E) && seeMrune == true)
        {
            collectMRune = true;
            print("Collected Magnet Rune");
            Destroy(other.gameObject);
                magnetText.enabled = true;
        }

    }

}
