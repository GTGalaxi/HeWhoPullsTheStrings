using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public void ResumeButton()
    {
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        Application.LoadLevel(0);
    }

    public void OptionsButton()
    {
        print("THERE ARE NO OPTIONS");
    }
}
