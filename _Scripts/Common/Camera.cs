using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Camera {

	public static void SetCamera(GameObject camera) {
        camera.transform.position = new Vector3(Game.Col / 2, Game.GetMaxCoord(), Game.Row / 5);
	}

}
