using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectingrune : MonoBehaviour {
    
    public static int NoOfRunes = 0;
    public int NoOfRunes2 = 0;
    private bool setting;

    // Use this for initialization
    void Start () {

        setting = false;
        RuneInventory.runesAccessable[0] = false;
        RuneInventory.runesAccessable[1] = false;
        RuneInventory.runesAccessable[2] = false;
        RuneInventory.runesAccessable[3] = false;


    }
	
	// Update is called once per frame
	void Update () {

        NoOfRunes2 = NoOfRunes;

        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[0] = true;
        if (NoOfRunes >= 1)
            RuneInventory.runesAccessable[1] = true;
        if (NoOfRunes >= 2)
            RuneInventory.runesAccessable[2] = true;
        if (NoOfRunes >= 3)
            RuneInventory.runesAccessable[3] = true;

        if(setting == true)
        {
            NoOfRunes++;
            setting = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();

        if (hitBoxScript.HitRune == true)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                setting = true;
                Application.LoadLevel(Application.loadedLevel +1);
            }

        }
    }
}
