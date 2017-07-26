using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : DinamicObjectGenerator {

    public override void Generate(GameObject gameObject)
    {
        System.Random randomValue = new System.Random();
        int currentCountOfEnemies = 0;

        
        while (currentCountOfEnemies < Game.countOfEnemies)
        {
            int row = randomValue.Next(1, Game.row);
            int col = randomValue.Next(1, Game.col);
            
            if (Game.matrixMap[row, col] == (int)ObjectType.Empty)
            {
                Game.AddObjectToMap(gameObject, new Vector3(col, 1, row), ObjectType.Enemy);
                currentCountOfEnemies++;
            }
        }
    }


}
