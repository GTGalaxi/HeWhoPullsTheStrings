using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

    public GameObject player;
    public float speed;
    public bool activated = false;
    public float magnetRange =5;
    
    public int keyRef;
    public MagnetRune magnetRune;
    public RuneInventory runeInventory;

    void Start()
    {
        player = GameObject.Find("Player_Character");
        magnetRune = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MagnetRune>();

        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject thePlayer = GameObject.Find("Hitbox");
        MagnetCollision magnetScript = thePlayer.GetComponent<MagnetCollision>();

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (magnetScript.HitTarget == true && Input.GetMouseButton(0) && runeInventory.hoveredRune == 2)
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

}