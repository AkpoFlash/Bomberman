using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnvironmentGenerator : MonoBehaviour {

    protected bool IsBounded(int i, int j)
    {
        return i == 0 || j == 0 || i == Map.row - 1 || j == Map.col - 1;
    }

    protected bool IsEvenCell(int i, int j)
    {
        return !(i % 2 != 0 || j % 2 != 0);
    }

    protected bool CanSetBreakWall(int i, int j, int currentCountOfWall)
    {
        return Map.matrixMap[i, j] == (int)ObjectType.Empty
                && !(i == 1 && j == 1 || i == 1 && j == 2 || i == 2 && j == 1)
                && currentCountOfWall < Map.countOfBreakWall;
    }

}
