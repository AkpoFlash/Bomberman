using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Camera(GameObject camera) {
        camera.transform.position = new Vector3(Map.col / 2, Map.GetMaxCoord(), Map.row / 5);
	}

}
