using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiler : MonoBehaviour {



	// Use this for initialization
	void Start () {

        if (transform.lossyScale.x > transform.lossyScale.y)
        {
            gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1F * gameObject.transform.lossyScale.x, 0.1F * gameObject.transform.lossyScale.y);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1F * gameObject.transform.lossyScale.z, 0.1F * gameObject.transform.lossyScale.y);
        }
    }
	
	// Update is called once per frame
	void Update () {


        
    }
}
