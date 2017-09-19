using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetingsystem : MonoBehaviour {

    public float heightMultiplier;
    public float sightDist = 10;
    public Material newMtrl;
    public Material oldMtrl;
    public GameObject[] obj;

    // Use this for initialization
    void Start () {
        obj = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist, layerMask))
        {
            if (hit.collider.gameObject.tag == "Target")
            {
            }
            else
            {
            }
        }
        else
        {
        }
    }
}
