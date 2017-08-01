using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakWallGenerator : EnvironmentGenerator
{

    protected override int StartCoordinate { get { return 0; } }
    protected override int EndCoordinate { get { return Game.Row; } }
    protected override float PositionOnY { get { return 0.5f; } }

    protected override ObjectType TypeOfObject { get { return ObjectType.UnbreakWall; } }

    protected override bool IsCellAvailable(int row, int col)
    {
        return IsBounded(row, col) || IsEvenCell(row, col);
    }

}
