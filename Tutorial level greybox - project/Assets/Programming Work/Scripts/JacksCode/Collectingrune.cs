using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectingrune : MonoBehaviour {
    
    public static int NoOfRunes = 0;
    public int NoOfRunes2 = 0;
    private bool setting;
    public AsyncOperation async;
    public GameObject LoadingScreen;
    public Player_Movement Player;
    public bool Loading = false;
    public string NextScene;

    // Use this for initialization
    void Start () {

        setting = false;
        RuneInventory.runesAccessable[0] = false;
        RuneInventory.runesAccessable[1] = false;
        RuneInventory.runesAccessable[2] = false;
        RuneInventory.runesAccessable[3] = false;

        Player = GameObject.Find("Player_Character").GetComponent<Player_Movement>();
        LoadingScreen = GameObject.Find("BlackLoadScreen");
        LoadingScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        NoOfRunes2 = NoOfRunes;

        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[0] = true;
        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[1] = true;
        if (NoOfRunes >= 2)
            RuneInventory.runesAccessable[2] = true;
        if (NoOfRunes >= 0)
            RuneInventory.runesAccessable[3] = true;

        if(setting == true)
        {
            NoOfRunes++;
            setting = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hitBox = GameObject.Find("Hitbox");
        MagnetCollision hitBoxScript = hitBox.GetComponent<MagnetCollision>();

        if (hitBoxScript.HitRune == true)
        {

            if(Input.GetKeyDown(KeyCode.E))
            {
                setting = true;
                StartCoroutine(LoadNextScene(false));
            }

        }

        if (hitBoxScript.HitLastRune == true)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                setting = true;
                StartCoroutine(LoadNextScene(true));
            }

        }

    }

    public void SkipScene(bool last = false)
    {
        StartCoroutine(LoadNextScene(last));
    }

    IEnumerator LoadNextScene(bool LastLevel)
    {
        if (Loading == false)
        {
            LoadingScreen.SetActive(true);
            Loading = true;
            Player.enabled = false;
            yield return new WaitForSeconds(1);
            if (LastLevel != true)
            {
                async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

            }
            else
            {
                async = SceneManager.LoadSceneAsync("WinState", LoadSceneMode.Single);
            }
            async.allowSceneActivation = true;
            while (!async.isDone)
            {
                print("Loading");
                yield return null;
            }
        }
    }
}
