using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRune2 : MonoBehaviour {

    public Shader shader1;
    public Shader shader2;

    public Renderer rend;


    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        shader1 = Shader.Find("Rune/AlwaysVisible");
        shader2 = Shader.Find("Diffuse");
    }
	
	// Update is called once per frame
	void Update () {

        GameObject Player_Character = GameObject.Find("Player_Character");
        visionRune seeThrough = Player_Character.GetComponent<visionRune>();

        if(seeThrough.seeRunes == true)
        rend.material.shader = shader1;
        
        if(seeThrough.seeRunes == false)
        rend.material.shader = shader2;

    }
}
