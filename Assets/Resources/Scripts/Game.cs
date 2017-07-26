using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game {

    public static int row;

    public static int col;

    public static int countOfBreakWall;

    public static int countOfEnemies;

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

    public static void AddObjectToMap(GameObject gameObject, Vector3 position, ObjectType objectType)
    {
        MonoBehaviour.Instantiate(gameObject, position, Quaternion.identity);
        Game.matrixMap[(int)position.z, (int)position.x] = (int)objectType;
    }

}
