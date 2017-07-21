using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakWallGenerator : EnvironmentGenerator
{

    public void Generate(string prefabName)
    {
        UnbreakWallLoader unbreakWallPrefab = new UnbreakWallLoader();

        for (int i = 0; i < Map.row; i++)
        {
            for (int j = 0; j < Map.col; j++)
            {
                if (IsBounded(i, j) || IsEvenCell(i, j))
                {
                    Instantiate(unbreakWallPrefab.GetElement(prefabName), new Vector3(j, 0.5f, i), Quaternion.identity);
                    Map.matrixMap[i, j] = (int)ObjectType.UnbreakWall;
                }
            }
        }
    }

}
