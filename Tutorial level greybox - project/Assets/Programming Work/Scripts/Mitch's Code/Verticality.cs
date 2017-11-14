using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verticality : MonoBehaviour {

    public GameObject player;
    public GameObject vertCollider;
    public AnimationClip vault;
    public GameObject vertPad;

	// Use this for initialization
	void Start () {
        vertCollider = GameObject.FindGameObjectWithTag("VertCollider");
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == vertCollider)
        {
            if (Input.GetAxis("Jump") == 1)
            {
                player.GetComponent<Player_Movement>().enabled = false;
                player.GetComponent<Animation>().clip = vault;
                player.GetComponent<Animation>().CrossFade(vault.name, 0.2F, PlayMode.StopAll);
                StartCoroutine(stallJump(0.6f));
                StartCoroutine(stall(1f));
            }
        }
    }

    IEnumerator stallJump(float timer)
    {
        yield return new WaitForSeconds(timer);
        player.GetComponent<Rigidbody>().transform.position = Vector3.Slerp(player.transform.position, vertPad.transform.position, 10 * Time.deltaTime);
    }

    IEnumerator stall(float timer)
    {
        yield return new WaitForSeconds(timer);
        player.GetComponent<Player_Movement>().enabled = true;
    }
}
