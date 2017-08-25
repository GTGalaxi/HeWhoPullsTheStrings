using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmRune : MonoBehaviour {



    private GameObject Ai;
    private GameObject character;


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

        
        KnockOutRune();
        

    }


    // Press input deactivate ai script

    void KnockOutRune()
    {

        if (Input.GetKey(KeyCode.E))
        {
           print("E has been pressed");
            // Destroy(GameObject.FindWithTag("AI"));
            
            Ai.GetComponent<UnityStandardAssets.Characters.ThirdPerson.TestScriptII>().enabled = false;

            Ai.GetComponent<NaveMesh>().enabled = false;
        }
        else
        //if (Input.GetKeyUp(KeyCode.E))
        {
            Ai.GetComponent<UnityStandardAssets.Characters.ThirdPerson.TestScriptII>().enabled = true ;

            Ai.GetComponent<NaveMesh>().enabled = true;
        }



    }


}
