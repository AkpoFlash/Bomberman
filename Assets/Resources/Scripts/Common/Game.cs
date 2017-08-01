using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{

    public static int Row { get; set; }

    public static int Col { get; set; }

    public static int CountOfBreakWall { get; set; }

    public static int CountOfEnemies { get; set; }

    public static int DinamicObjectSmooth { get; set; }

    public static float DinamicObjectSpeed { get; set; }

    public static double probabilityAppearanceBreakWall = 0.3;

    public static int[,] MatrixMap { get; set; }

    public static int GetMaxCoord()
    {
        return (Row < Col) ? Col : Row;
    }

    public static bool CheckRandomAppearance(double probabilityAppearance, System.Random randomValue)
    {
        return randomValue.NextDouble() < probabilityAppearance;
    }

    public static GameObject AddObjectToMap(GameObject gameObject, Vector3 position, ObjectType objectType)
    {
        Game.MatrixMap[(int)position.z, (int)position.x] = (int)objectType;
        return MonoBehaviour.Instantiate(gameObject, position, Quaternion.identity);
    }

}
