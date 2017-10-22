using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionRune : MonoBehaviour {

    public bool seeRunes = false;

    // Use this for initialization
    void Start ()
    {
		


	}

    // Update is called once per frame
    void Update()
    {


        GameObject[] Runes = GameObject.FindGameObjectsWithTag("Rune");
        foreach (GameObject Rune in Runes)
        {

            if (Input.GetMouseButtonDown(0))
            {

                    seeRunes = !seeRunes;

            }

            if (seeRunes == true)
                Rune.GetComponent<Renderer>().material.color = new Color(0, 0, 255);

            if (seeRunes == false)
                Rune.GetComponent<Renderer>().material.color = new Color(255, 99, 216);

        }
    }
}
