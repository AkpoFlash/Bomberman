using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour {

    public void Generate(string prefabName)
    {
        PlayerLoader playerPrefab = new PlayerLoader();

        Instantiate(playerPrefab.GetElement(prefabName), new Vector3(1, 1, 1), Quaternion.identity);
        Map.matrixMap[1, 1] = (int)ObjectType.Player;
    }

}
