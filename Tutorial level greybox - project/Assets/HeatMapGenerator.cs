using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class heatMapSize
{
    public int sizeX = 100;
    public int sizeZ = 100;
}

public class HeatMapGenerator : MonoBehaviour {

    public float heatMapResolution = 2;
    public heatMapSize heatMapSize = new heatMapSize();
    public GameObject HeatCube;
    public GameObject[,] HeatCubeArray;
    public RenderTexture heatMapInput;
    public Camera heatMapCamera;
    private bool holder = false;

	// Use this for initialization
	void Start () {
        float resolution = 4 / heatMapResolution ;
        Debug.Log(resolution);
        heatMapSize.sizeX = heatMapSize.sizeX * Mathf.RoundToInt(resolution + 1);
        heatMapSize.sizeZ = heatMapSize.sizeZ * Mathf.RoundToInt(resolution + 1);
        HeatCubeArray = new GameObject[heatMapSize.sizeX, heatMapSize.sizeZ];

        for (int i = -heatMapSize.sizeX/2; i < heatMapSize.sizeX/2; i++)
        {
            for (int ii = -heatMapSize.sizeZ/2; ii < heatMapSize.sizeZ/2; ii++)
            {
                HeatCubeArray[i + heatMapSize.sizeX / 2, ii + heatMapSize.sizeZ / 2] = Instantiate(HeatCube, new Vector3(gameObject.transform.position.x + i * resolution, gameObject.transform.position.y, gameObject.transform.position.z + ii * resolution), new Quaternion(0, 0, 0, 0));
                HeatCubeArray[i + heatMapSize.sizeX / 2, ii + heatMapSize.sizeZ / 2].transform.parent = gameObject.transform;
                HeatCubeArray[i + heatMapSize.sizeX / 2, ii + heatMapSize.sizeZ / 2].transform.localScale = new Vector3(resolution, 4, resolution);
            }
        }
	}

    void UpdateHeatMap ()
    {
        for (int i = -heatMapSize.sizeX / 2; i < heatMapSize.sizeX / 2; i++)
        {
            for (int ii = -heatMapSize.sizeZ / 2; ii < heatMapSize.sizeZ / 2; ii++)
            {
                print(HeatCubeArray[i + heatMapSize.sizeX / 2, ii + heatMapSize.sizeZ / 2]);
                HeatCubeArray[i + heatMapSize.sizeX / 2, ii + heatMapSize.sizeZ / 2].GetComponent<HeatMap>().HeatMapUpdate();
            }
        }

        heatMapCamera.aspect = 1.0f;
        heatMapCamera.Render();

        RenderTexture.active = heatMapInput;
        Texture2D heatMapOutput = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);

        heatMapOutput.ReadPixels(new Rect(0, 0, 2048, 2048), 0, 0);
        RenderTexture.active = null;

        byte[] bytes;
        bytes = heatMapOutput.EncodeToPNG();

        System.IO.File.WriteAllBytes("C:/tmp/heatMapOutput_"+ SceneManager.GetActiveScene().name +".png", bytes);

    }

    void Update ()
    {
        if(Input.GetKey(KeyCode.K) && holder == false)
        {
            holder = true;
            UpdateHeatMap();
            holder = false;
        }
    }

    IEnumerator stall(float timer)
    {
        yield return new WaitForSeconds(timer);
    }
}
