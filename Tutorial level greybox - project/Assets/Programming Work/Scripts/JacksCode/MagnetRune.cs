using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetRune : MonoBehaviour {
    
    public List<int> collectKey;
    public RuneInventory runeInventory;

    private void Start()
    {
        collectKey.Add(-1);
    }
    // Use this for initialization
    void FixedUpdate ()

    {

    }

    void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Key")
        {
            if (Input.GetMouseButton(0) && runeInventory.hoveredRune == 2)
            {
                collectKey.Add(other.gameObject.GetComponent<Key>().keyRef);
                print("Collected Key");
                Destroy(other.gameObject);
            }
        }
    }

}
