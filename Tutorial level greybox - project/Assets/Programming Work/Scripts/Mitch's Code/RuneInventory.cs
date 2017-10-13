using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneInventory : MonoBehaviour {

    //public  runes = {};
    public bool[] runesAccessable = { false , false , false , false };
    public GameObject[] runeButtons;

    public bool runeSelect = false;
    public int hoveredRune = 0;
    public bool inventoryOpen = false;
    public bool pause = false;
    public bool runeBreak = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("RuneSelector") > 0.1 )
        {
            if (pause == false)
                pause = true;
            if (runeSelect == false)
            {
                for (int i = 0; i < runeButtons.Length; i++)
                {
                    runeButtons[i].SetActive(true);
                }
                runeSelect = true;
            }

            float selectionAngle = Mathf.Atan(Input.GetAxis("Vertical") / Input.GetAxis("Horizontal")) * Mathf.Rad2Deg;

            if (selectionAngle <-67.5 || selectionAngle >= 67.5)
            {
                hoveredRune = 1;
            }
            else if (selectionAngle >= 22.5)
            {
                hoveredRune = 2;
            }
            else if (selectionAngle >= -22.5 )
            {
                hoveredRune = 3;
            }
            else if (selectionAngle >= -67.5)
            {
                hoveredRune = 4;
            }
            else
            {
                hoveredRune = 0;
            }
            print(hoveredRune);
            
            for (int ii = 0; ii < runeButtons.Length; ii++)
            {
                if (ii+1 == hoveredRune)
                {
                    runeButtons[ii].GetComponent<RectTransform>().sizeDelta.Set(50F, 50F);
                }
                else
                {
                    runeButtons[ii].GetComponent<RectTransform>().sizeDelta.Set(40F, 40F);
                }
            }
            
        }
        else
        {
            if (runeSelect == true)
            {
                for (int i = 0; i < runeButtons.Length; i++)
                {
                    runeButtons[i].SetActive(false);
                }
                runeSelect = false;
            }
        }
        if (Input.GetAxis("RuneSelector") < 0.1)
        {
            
        }
	}
}