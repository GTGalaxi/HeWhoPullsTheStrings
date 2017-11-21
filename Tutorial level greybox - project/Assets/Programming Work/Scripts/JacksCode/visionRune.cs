using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionRune : MonoBehaviour {
    
    public bool seeRunes = false;

    public RuneInventory runeInventory;

    // Use this for initialization
    void Start ()
    {
<<<<<<< HEAD
        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
=======
        GameObject runeInventory = GameObject.Find("RuneImage");
>>>>>>> a75381e71a8bb3597858d45415983bfb4b2dc3ad
        seeRunes = false;

    }

    // Update is called once per frame
    void Update()
    {


        GameObject[] Runes = GameObject.FindGameObjectsWithTag("Rune");
        foreach (GameObject Rune in Runes)
        {
            if (Input.GetMouseButtonDown(0) && runeInventory.hoveredRune == 1)
            {

                Debug.Log("did this");
                seeRunes = !seeRunes;

            }

            if (runeInventory.hoveredRune != 1)
                seeRunes = false;


        }
    }
}
