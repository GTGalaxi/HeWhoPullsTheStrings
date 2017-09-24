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

        float distance = Vector3.Distance(transform.position, target.position);
        print(distance);

        if (magnetrune.collectMRune == true && Input.GetMouseButton(0))
        {

            activated = true;
            magnetRunetext.enabled = false;


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

}
