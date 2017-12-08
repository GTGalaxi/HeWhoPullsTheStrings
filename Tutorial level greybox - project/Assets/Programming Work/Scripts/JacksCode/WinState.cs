using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour {

    void Start()
    {
        Cursor.visible = true;
    }
    public void HomeButton()
    {
        Application.LoadLevel("MainMenu");
    }

}
