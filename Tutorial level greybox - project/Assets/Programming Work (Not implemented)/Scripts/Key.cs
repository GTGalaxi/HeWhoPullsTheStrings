using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

    public GameObject player;
    public float speed;
    public bool activated = false;
    public float magnetRange =5;
    public Transform target;

    public MagnetRune magnetrune;
    public Text magnetRunetext;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject thePlayer = GameObject.Find("MainCamera");
        MagnetRune magnetScript = thePlayer.GetComponent<MagnetRune>();


        float distance = Vector3.Distance(transform.position, target.position);

        if (magnetScript.HitTarget == true && Input.GetMouseButton(0))
        {

            activated = true;

        }


        if (distance < magnetRange)
        {
            if (activated == true)
            {
                transform.LookAt(player.transform.position);

                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.tag == "Player")
        {
            print("Collected Key");
            Destroy(gameObject);

        }



    }

}
