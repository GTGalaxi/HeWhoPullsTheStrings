using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ImmRune : MonoBehaviour {



    private GameObject Ai;

   private float runeCooldown = 7f;
    private float timer  = 7f;
    public float runeDuration = 10f;
   

    // Use this for initialization
    void Start ()
    {
       
       // character = GameObject.FindGameObjectWithTag("Player");
      //  character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.TestScriptII>().patrolSpeed();
        Ai = GameObject.FindGameObjectWithTag("AI");
        //Ai.SetActive(true);

    }
	
	// Update is called once per frame
	void Update ()
    {



        


        //adds a five sec puffer to button press


        KnockOutRune();

        


    }





    // Press input deactivate ai script

    void KnockOutRune()
    {


        runeDuration -= 1 * Time.deltaTime;



        if (Input.GetKeyDown(KeyCode.E) && runeDuration >= 0.0f)
        {

            print("E has been pressed");

            




            Ai.GetComponent<Script_General_AI>().enabled = false;
            Ai.GetComponent<NavMeshAgent>().enabled = false;

            timer = runeCooldown;

        }
        else if (timer <= 0.0f || runeDuration <= 0)
        //if (Input.GetKeyUp(KeyCode.
        {
            runeDuration = 10;
            
            Ai.GetComponent<Script_General_AI>().enabled = true;
            Ai.GetComponent<NavMeshAgent>().enabled = true;

        }
     
    }


        
}
      






    



