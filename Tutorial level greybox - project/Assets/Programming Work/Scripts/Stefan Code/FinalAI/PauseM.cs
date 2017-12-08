using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseM : MonoBehaviour {


    public Transform PauseBackGround;
    public Transform Player;
    public Transform ControlCanvas;
   





	// Use this for initialization
	void Start () {


        ControlCanvas.gameObject.SetActive(false);
        PauseBackGround.gameObject.SetActive(false);

	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {

            PauseT();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {

            Controls();
        }


    }





    public void PauseT()
    {

        if (PauseBackGround.gameObject.activeInHierarchy == false)
        {
            PauseBackGround.gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            ControlCanvas.gameObject.SetActive(false);

            Player.GetComponent<Player_Movement>().enabled = false;
          
        }

       else
        {
            PauseBackGround.gameObject.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            ControlCanvas.gameObject.SetActive(false);

            Player.GetComponent<Player_Movement>().enabled = true;
            
        }





    }

    public void MainM(bool Quit)
    {
        
            SceneManager.LoadScene("MainMenu");
      
    }


    public void Controls()
    {


        if (ControlCanvas.gameObject.activeInHierarchy == false)
        {
            ControlCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            //  PauseBackGround.gameObject.SetActive(false);
            PauseBackGround.gameObject.SetActive(true);
            Player.GetComponent<Player_Movement>().enabled = false;

        }

        else
        {
            ControlCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;

            //PauseBackGround.gameObject.SetActive(false);
            Player.GetComponent<Player_Movement>().enabled = true;

        }


    }















}
