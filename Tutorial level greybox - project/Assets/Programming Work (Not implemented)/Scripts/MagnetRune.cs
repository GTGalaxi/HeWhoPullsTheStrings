using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetRune : MonoBehaviour {
    
    public bool collectKey = true;


    // Use this for initialization
    void FixedUpdate ()

    {

    }

    void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Key")
        {
            collectKey = true;
            print("Collected Key");
            Destroy(other.gameObject);

        }
    }

}
