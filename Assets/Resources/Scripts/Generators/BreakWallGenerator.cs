using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallGenerator : EnvironmentGenerator
{

    protected override int StartCoordinate { get { return 2; } }
    protected override int MaxRowCoordinate { get { return Game.row - 2; } }
    protected override int MaxColCoordinate { get { return Game.col - 2; } }
    protected override float PositionOnY { get { return 0.5f; } }

    protected override ObjectType TypeOfObject { get { return ObjectType.BreakWall; } }

    protected override bool IsCellAvailable(int row, int col)
    {
        return CanSetBreakWall(row, col, 0) && IsEvenCell(row - 1, col - 1);
    }

    private bool CanSetBreakWall(int i, int j, int currentCountOfWall)
    {
        return Game.matrixMap[i, j] == (int)ObjectType.Empty
                && !(i == 1 && j == 1 || i == 1 && j == 2 || i == 2 && j == 1)
                && currentCountOfWall < Game.CountOfBreakWall;
    }

}
