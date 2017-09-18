using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetingsystem : MonoBehaviour {

    public float heightMultiplier;
    public float sightDist = 10;
    Renderer objRenderer;
    public Material newMtrl;
    public Material oldMtrl;
    public GameObject obj;

    // Use this for initialization
    void Start () {
        objRenderer = obj.GetComponent<Renderer>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Target")
            {
                objRenderer.material = newMtrl;
            }
            else
            {
                objRenderer.material = oldMtrl;
            }
        }
        else
        {
            objRenderer.material = oldMtrl;
        }
    }
}
