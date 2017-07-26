using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera {

	public Camera(GameObject camera) {
        camera.transform.position = new Vector3(Game.col / 2, Game.GetMaxCoord(), Game.row / 5);
	}

}
