using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallRandomGenerator : EnvironmentGenerator
{

    protected override int StartCoordinate { get { return 1; } }
    protected override int EndCoordinate { get { return Game.Row - 1; } }
    protected override float PositionOnY { get { return 0.5f; } }
    protected override ObjectType TypeOfObject { get { return ObjectType.BreakWall; } }

    private int CurrentCountOfWall { get; set; }
    private System.Random randomValue = new System.Random();

    protected override bool IsCellAvailable(int row, int col)
    {
        return CanSetBreakWall(row, col) && Game.CheckRandomAppearance(Game.probabilityAppearanceBreakWall, this.randomValue) && this.CurrentCountOfWall++ < Game.CountOfBreakWall;
    }

    private bool CanSetBreakWall(int i, int j)
    {
        return Game.MatrixMap[i, j] == (int)ObjectType.Empty && !(i == 1 && j == 1 || i == 1 && j == 2 || i == 2 && j == 1);
    }

}
