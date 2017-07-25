using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour {

    public void Generate(string prefabName)
    {
        PlayerLoader playerPrefab = new PlayerLoader();
        System.Random randomValue = new System.Random();


        while (true)
        {
            int row = randomValue.Next(1, Map.row - 1);
            int col = randomValue.Next(1, Map.col - 1);
            Debug.Log(row + " " + col);
            if (Map.matrixMap[row, col] == (int)ObjectType.Empty)
            {
                Instantiate(playerPrefab.GetElement(prefabName), new Vector3(col, 1, row), Quaternion.identity);
                Map.matrixMap[row, col] = (int)ObjectType.Enemy;
                break;
            }
        }
    }

}
