using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectingrune : MonoBehaviour {
    
    public static int NoOfRunes = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[0] = true;
        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[1] = true;
        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[2] = true;
        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[3] = true;


    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();

        if (hitBoxScript.HitRune == true)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                NoOfRunes++;
                Application.LoadLevel(Application.loadedLevel +1);
            }

        }
    }
}
