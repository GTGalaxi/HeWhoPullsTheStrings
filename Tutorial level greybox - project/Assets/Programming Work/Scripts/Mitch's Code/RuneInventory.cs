using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneInventory : MonoBehaviour {

    //public  runes = {};
    [SerializeField]
    public static bool[] runesAccessable = { false , false , false , false };
    public GameObject[] runeButtons;

    public bool runeSelect = false;
    public int hoveredRune = 0;
    public bool inventoryOpen = false;
    public bool pause = false;
    public bool runeBreak = false;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < runeButtons.Length; i++)
        {
            runeButtons[i].SetActive(false);
        }
        runeSelect = false;
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
            print(selectionAngle);

            if (selectionAngle <-67.5 && runesAccessable[0] == true || selectionAngle >= 67.5 && runesAccessable[0] == true)
            {
                hoveredRune = 1;
            }
            else if (selectionAngle >= 22.5 && runesAccessable[1] == true)
            {
                hoveredRune = 2;
            }
            else if (selectionAngle >= -22.5 && runesAccessable[2] == true)
            {
                hoveredRune = 3;
            }
            else if (selectionAngle >= -67.5 && runesAccessable[3] == true)
            {
                hoveredRune = 4;
            }
            else
            {
                hoveredRune = 0;
            }
            
            
        }

        else if (Input.GetKey(KeyCode.Alpha0))
        {
            if (pause == false)
                pause = true;
            for (int i = 0; i < runeButtons.Length; i++)
            {
                if (i == 0)
                    runeButtons[i].SetActive(true);
                else
                    runeButtons[i].SetActive(false);
            }
            hoveredRune = 0;
            runeSelect = true;
        }

        else if(Input.GetKey(KeyCode.Alpha1))
        {
            if (pause == false)
                pause = true;
            for (int i = 0; i < runeButtons.Length; i++)
            {
                if (i == 0)
                    runeButtons[i].SetActive(true);
                else
                    runeButtons[i].SetActive(false);
            }
            if (runesAccessable[0] == true)
            {
                hoveredRune = 1;
            }
            runeSelect = true;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (pause == false)
                pause = true;
            for (int i = 0; i < runeButtons.Length; i++)
            {
                if (i == 1)
                    runeButtons[i].SetActive(true);
                else
                    runeButtons[i].SetActive(false);
            }
            if (runesAccessable[1] == true)
            {
                hoveredRune = 2;
            }
            runeSelect = true;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (pause == false)
                pause = true;
            for (int i = 0; i < runeButtons.Length; i++)
            {
                if (i == 2)
                    runeButtons[i].SetActive(true);
                else
                    runeButtons[i].SetActive(false);
            }
            if (runesAccessable[2] == true)
            {
                hoveredRune = 3;
            }
            runeSelect = true;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (pause == false)
                pause = true;
            for (int i = 0; i < runeButtons.Length; i++)
            {
                if (i == 3)
                    runeButtons[i].SetActive(true);
                else
                    runeButtons[i].SetActive(false);
            }
            if (runesAccessable[3] == true)
            {
                hoveredRune = 4;
            }
            runeSelect = true;
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
            pause = false;
        }

    }
}