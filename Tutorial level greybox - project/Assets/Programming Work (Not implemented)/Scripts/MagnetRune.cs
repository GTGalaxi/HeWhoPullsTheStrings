using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetRune : MonoBehaviour {
    
    public List<int> collectKey;

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
            print("Collected Key");
            Destroy(other.gameObject);

        }
    }

}
