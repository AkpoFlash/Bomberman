using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public void Generate(string prefabName)
    {
        EnemyLoader enemyPrefab = new EnemyLoader();
        System.Random randomValue = new System.Random();
        int currentCountOfEnemies = 0;

        
        while (currentCountOfEnemies < Map.countOfEnemies)
        {
            int row = randomValue.Next(1, Map.row - 1);
            int col = randomValue.Next(1, Map.col - 1);
            Debug.Log(row + " " + col);
            if (Map.matrixMap[row, col] == (int)ObjectType.Empty)
            {
                Instantiate(enemyPrefab.GetElement(prefabName), new Vector3(col, 1, row), Quaternion.identity);
                Map.matrixMap[row, col] = (int)ObjectType.Enemy;
                currentCountOfEnemies++;
            }
        }
    }


}
