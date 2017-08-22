using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : EnvironmentGenerator
{

    protected override int StartCoordinate { get { return 0; } }
    protected override int MaxRowCoordinate { get { return Game.row; } }
    protected override int MaxColCoordinate { get { return Game.col; } }
    protected override float PositionOnY { get { return 0f; } }

    protected override ObjectType TypeOfObject { get { return ObjectType.Empty; } }

    protected override bool IsCellAvailable(int row, int col)
    {
        return true;
    }

}
