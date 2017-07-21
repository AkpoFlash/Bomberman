using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallGenerator : EnvironmentGenerator
{

    public void Generate(string prefabName)
    {
        BreakWallLoader breakWallPrefab = new BreakWallLoader();

        for (int i = 1; i < Map.row - 1; i++)
        {
            for (int j = 1; j < Map.col - 1; j++)
            {
                if (CanSetBreakWall(i, j, 0) && IsEvenCell(i-1, j-1))
                {
                    Instantiate(breakWallPrefab.GetElement(prefabName), new Vector3(j, 0.5f, i), Quaternion.identity);
                    Map.matrixMap[i, j] = (int)ObjectType.BreakWall;
                }
            }
        }
    }

    public void GenerateRandom(string prefabName)
    {
        BreakWallLoader breakWallPrefab = new BreakWallLoader();
        System.Random randomValue = new System.Random();
        int currentCountOfWall = 0;

        for (int i = 1; i < Map.row - 1; i++)
        {
            for (int j = 1; j < Map.col - 1; j++)
            {
                if (CanSetBreakWall(i, j, currentCountOfWall) && Map.CheckRandomAppearance(Map.probabilityAppearanceBreakWall, randomValue))
                {
                    Instantiate(breakWallPrefab.GetElement(prefabName), new Vector3(j, 0.5f, i), Quaternion.identity);
                    Map.matrixMap[i, j] = (int)ObjectType.BreakWall;
                    currentCountOfWall++;
                }
            }
        }
    }

}
