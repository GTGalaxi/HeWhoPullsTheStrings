using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    public void QuitButton ()
    {
        Application.Quit();
	}

    public void StartButton ()
    {
        Application.LoadLevel(2);
	}

    public void OptionsButton()
    {
        print("THERE ARE NO OPTIONS");
    }
}
