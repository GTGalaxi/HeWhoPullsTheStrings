using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour {

    public float colourTime = 0;
    public float totalTime = 0;
    [SerializeField]
    public static float greatestTime = 0;
    public Gradient heatGradient;
    public Renderer heatColour;

    // Use this for initialization
    void Start () {
        greatestTime = 0;

        GradientColorKey[] gck = new GradientColorKey[5];
        GradientAlphaKey[] gak = new GradientAlphaKey[5];

        gck[0].color = Color.blue; gck[0].time = 0.0F;
        gck[1].color = Color.cyan; gck[1].time = 0.25F;
        gck[2].color = Color.green; gck[2].time = 0.5F;
        gck[3].color = Color.yellow; gck[3].time = 0.75F;
        gck[4].color = Color.red; gck[4].time = 1.0F;

        gak[0].alpha = 1.0F; gak[0].time = 0.0F;
        gak[1].alpha = 1.0F; gak[1].time = 0.25F;
        gak[2].alpha = 1.0F; gak[2].time = 0.5F;
        gak[3].alpha = 1.0F; gak[3].time = 0.75F;
        gak[4].alpha = 1.0F; gak[4].time = 1.0F;

        heatGradient.SetKeys(gck, gak);
        heatColour = gameObject.GetComponent<Renderer>();

    }

	public void HeatMapUpdate() {

        if (colourTime > greatestTime)
        {
            greatestTime = colourTime;
        }
        
        heatColour.material.color = heatGradient.Evaluate((colourTime / greatestTime) * 10);
        StartCoroutine(stall(1));
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "VertCollider")
        {
            colourTime += Time.deltaTime/2;
        }
    }

    IEnumerator stall(float timer)
    {
        yield return new WaitForSeconds(timer);
    }

}
