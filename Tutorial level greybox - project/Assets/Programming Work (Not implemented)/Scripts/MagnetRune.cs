using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetRune : MonoBehaviour {
    
    //Raycasting
    public float heightMultiplier;
    public float sightDist = 10;
    
    public bool collectMRune = true;
    public bool collectKey = true;
    public int timeSinceCollision;
    public bool seeMrune = true;
    public bool HitTarget = false;

    public Text visionRune;
    public Text magnetRunetext;


    // Use this for initialization
    void FixedUpdate ()

    {

        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist, layerMask))
        {
            if (hit.collider.gameObject.tag == "Target")
            {
                print("hit");
                HitTarget = true;
            }
            else
            {
                print("not hit");
                HitTarget = false;
            }
        }
        else
        {
            print("not hit");
            HitTarget = false;
        }

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
