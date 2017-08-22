using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Game
{
    [SyncVar]
    public static int row;

    [SyncVar]
    public static int col;

    public static int CountOfBreakWall { get; set; }

    public static int CountOfEnemies { get; set; }

    public static int CountOfProEnemies { get; set; }

    public static int DinamicObjectSmooth { get; set; }

    public static float DinamicObjectSpeed { get; set; }

    public static double probabilityAppearanceBreakWall = 0.3;

    public static double probabilityAppearancePowerUp = 0.5;

    [SyncVar]
    public static int[,] matrixMap;

    public static GameObject GUI;

    public static int GetMaxCoord()
    {
        return (row < col) ? col : row;
    }

    public static bool CheckRandomAppearance(double probabilityAppearance, System.Random randomValue)
    {
        return randomValue.NextDouble() < probabilityAppearance;
    }

    public static GameObject AddObjectToMap(GameObject gameObject, Vector3 position, ObjectType objectType)
    {
        Game.matrixMap[(int)position.z, (int)position.x] = (int)objectType;
        GameObject obj = MonoBehaviour.Instantiate(gameObject, position, Quaternion.identity);
        NetworkServer.Spawn(obj);
        return obj;
    }

}
