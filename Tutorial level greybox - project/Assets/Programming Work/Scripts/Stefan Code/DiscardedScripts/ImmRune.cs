using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ImmRune : MonoBehaviour {

    public RuneInventory runeInventory;

    private GameObject Ai;

    private float runeCooldown = 10f;
    public float timer  = 10;
    public float runeDuration = 5f;
    public GameObject hitBox;
    public bool immobilised = false;
   

    // Use this for initialization
    void Start ()
    {
        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();

        timer -= Time.deltaTime;



        if (immobilised == true)
        {
            runeDuration -= Time.deltaTime;

            hitBoxScript.AIHit.GetComponent<DummyMaidAi>().enabled = false;
            timer = runeCooldown;



        }
        if (runeDuration <= 0.0f)
        {
            immobilised = false;
            hitBoxScript.AIHit.GetComponent<DummyMaidAi>().enabled = true;
            runeDuration = 5;
            timer = 10;



        }

        if (timer <= 0.0f && runeInventory.hoveredRune == 3 && hitBoxScript.HitTarget == true && immobilised == false)
        {
            immobilised = true;
        }
        
    }

        
}
      






    



