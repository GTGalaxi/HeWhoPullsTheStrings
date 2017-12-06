using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossState : MonoBehaviour {

    public void NoButton()
    {
        Application.LoadLevel("MainMenu");

        Debug.Log("Me too");
    }
    public void YesButton()
    {
        Application.LoadLevel(Application.loadedLevel);
        Debug.Log("Im working");
    }


}
