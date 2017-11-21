using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionRune : MonoBehaviour {
    
    public bool seeRunes = false;

    public RuneInventory runeInventory;

    // Use this for initialization
    void Start ()
    {
        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
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
