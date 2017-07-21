using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map {

    public static int row;

    public static int col;

    public static int countOfBreakWall;

    public static double probabilityAppearanceBreakWall = 0.3;

    public static int[,] matrixMap;

    public static int GetMaxCoord()
    {
        return (row < col) ? col : row;
    }

    public static bool CheckRandomAppearance(double probabilityAppearance, System.Random randomValue)
    {
        return randomValue.NextDouble() < probabilityAppearance;
    }

}
