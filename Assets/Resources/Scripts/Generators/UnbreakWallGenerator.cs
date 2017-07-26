using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakWallGenerator : EnvironmentGenerator
{

    public override void Generate(GameObject gameObject)
    {
        for (int i = 0; i < Game.row; i++)
        {
            for (int j = 0; j < Game.col; j++)
            {
                if (IsBounded(i, j) || IsEvenCell(i, j))
                {
                    Game.AddObjectToMap(gameObject, new Vector3(j, 0.5f, i), ObjectType.UnbreakWall);
                }
            }
        }
    }

}
