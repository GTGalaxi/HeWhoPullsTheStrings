using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetRune : MonoBehaviour {
    
    public List<int> collectKey;
    public RuneInventory runeInventory;
    public Text collectedKey;
    public float timer = 2;

    private void Start()
    {
        collectedKey.enabled = false;

        runeInventory = GameObject.Find("RuneImage").GetComponent<RuneInventory>();
        collectKey.Add(-1);
    }
    // Use this for initialization
    void FixedUpdate ()

    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            collectedKey.enabled = false;
        }

    }

    void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Key")
        {
            if (Input.GetMouseButton(0) && runeInventory.hoveredRune == 2)
            {
                collectKey.Add(other.gameObject.GetComponent<Key>().keyRef);
                print("Collected Key");
                collectedKey.enabled = true;
                timer = 2;
                Destroy(other.gameObject);
            }
        }
    }

}
