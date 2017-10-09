using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneInventory : MonoBehaviour {

    //public  runes = {};
    public bool[] runesAccessable = { false , false , false , false };
    public GameObject[] runeButtons;

    public bool runeSelect = false;
    public string currentRune = "";

	// Use this for initialization
	void Start () {
        currentRune = "vision";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("RuneSelector") > 0)
        {
            if (runeSelect == false)
            {
                for (int i = 0; i < runeButtons.Length; i++)
                {
                    runeButtons[i].SetActive(true);
                }
                runeSelect = true;
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
	}
}
