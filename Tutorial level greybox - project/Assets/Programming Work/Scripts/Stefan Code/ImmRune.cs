using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ImmRune : MonoBehaviour {



    private GameObject Ai;

   private float runeCooldown = 17f;
    private float timer  = 0;
    public float runeDuration = 10f;
   

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        GameObject hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();
        if (hitBoxScript.HitTarget == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && timer <= 0.0f)
            {
                runeDuration -= Time.deltaTime;


                hitBoxScript.AIHit.GetComponent<Maid_AI>().enabled = false;
                hitBoxScript.AIHit.GetComponent<NavMeshAgent>().enabled = false;
                timer = runeCooldown;
            }
            if (runeDuration <= 0.0f)
            {
                runeDuration = 10;

                hitBoxScript.AIHit.GetComponent<Maid_AI>().enabled = true;
                hitBoxScript.AIHit.GetComponent<NavMeshAgent>().enabled = true;

            }
        }
    }

        
}
      






    



