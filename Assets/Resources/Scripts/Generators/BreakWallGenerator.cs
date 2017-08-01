using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallGenerator : EnvironmentGenerator
{

    protected override int StartCoordinate { get { return 2; } }
    protected override int EndCoordinate { get { return Game.Row - 2; } }
    protected override float PositionOnY { get { return 0.5f; } }

    protected override ObjectType TypeOfObject { get { return ObjectType.BreakWall; } }

    protected override bool IsCellAvailable(int row, int col)
    {
        return CanSetBreakWall(row, col, 0) && IsEvenCell(row - 1, col - 1);
    }

    public void GenerateRandom(GameObject gameObject)
    {
        System.Random randomValue = new System.Random();
        int currentCountOfWall = 0;

        for (int i = 1; i < Game.Row - 1; i++)
        {
            for (int j = 1; j < Game.Col - 1; j++)
            {
                if (CanSetBreakWall(i, j, currentCountOfWall) && Game.CheckRandomAppearance(Game.probabilityAppearanceBreakWall, randomValue))
                {
                    Game.AddObjectToMap(gameObject, new Vector3(j, 0.5f, i), ObjectType.BreakWall);
                    currentCountOfWall++;
                }
            }
        }
    }

    private bool CanSetBreakWall(int i, int j, int currentCountOfWall)
    {
        return Game.MatrixMap[i, j] == (int)ObjectType.Empty
                && !(i == 1 && j == 1 || i == 1 && j == 2 || i == 2 && j == 1)
                && currentCountOfWall < Game.CountOfBreakWall;
    }

}
