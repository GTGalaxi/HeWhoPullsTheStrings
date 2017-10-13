using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    public MagnetRune magnetRune;
    public int keyAllocator;
    public bool opened;
    public Transform from;
    public Transform to;
    public GameObject pivotPoint;
    public bool pressed = false;
    public bool stay = false;
    public float doorRotation = 80;


	// Use this for initialization
	void Start () {
        magnetRune = GameObject.FindGameObjectWithTag("Player").GetComponent<MagnetRune>();
        to.rotation.Set(0, doorRotation, 0, 0);
	}
    private void Update()
    {
        if (stay)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!pressed)
                {
                    if (magnetRune.collectKey.Contains(keyAllocator))
                    {
                        Open();
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                pressed = false;
            }
        }        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stay = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stay = false;
            if (opened)
            {
                Open();
            }
        }
    }

    public void Open()
    {
        if (!opened)
        {
            pivotPoint.transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.time);
        }
        else if (opened)
        {
            pivotPoint.transform.rotation = Quaternion.Slerp(to.rotation, from.rotation, Time.time);
        }
        opened = !opened;
        pressed = true;
    }
}
