using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Camera
{
    [Command]
	public static void CmdSetCamera(GameObject camera) {
        camera.transform.position = new Vector3(Game.col / 2, Game.GetMaxCoord(), Game.row / 5);
	}

}
