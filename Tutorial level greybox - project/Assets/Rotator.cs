using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    void Update () {
        gameObject.transform.Rotate(0, 0, -10, 0);
	}
}
