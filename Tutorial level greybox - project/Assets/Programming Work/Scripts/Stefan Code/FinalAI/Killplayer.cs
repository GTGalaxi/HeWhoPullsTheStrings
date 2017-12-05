using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killplayer : MonoBehaviour {

    public GameObject lossState;
    public GameObject thePlayer;

    private void Awake()
    {

        lossState = GameObject.Find("LossState");
        GameObject lossCanvas = lossState.transform.GetChild(0).gameObject;
        lossCanvas.SetActive(false);
        thePlayer = GameObject.Find("Player_Character");
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        

	}

    

     void OnTriggerEnter(Collider other)
     { 
        if (other.gameObject.tag == "Player")
        {
            lossState = GameObject.Find("LossState");
            GameObject lossCanvas = lossState.transform.GetChild(0).gameObject;
            lossCanvas.SetActive(true);
            thePlayer.GetComponent<Player_Movement>().enabled = false;
        }
    }
}
