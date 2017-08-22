using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

    public GameObject mainCamera;

    public int rowCount = 15;
    public int colCount = 15;
    public int countOfBreakWall = 50;
    public int countOfEnemies = 3;
    public int countOfProEnemies = 1;
    public int dinamicObjectSmooth = 20;
    public float dinamicObjectSpeed = 5;

    public GameObject GUI;
    public GameObject groundPrefab;
    public GameObject breakWallPrefab;
    public GameObject unbreakWallPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemyProPrefab;
    public GameObject[] powerUpPrefab;

    public bool randomBreakWall = true;

    private MapGenerator map;

    private void Start()
    {
        Camera.CmdSetCamera(mainCamera);
        Game.DinamicObjectSpeed = this.dinamicObjectSpeed;
        Game.DinamicObjectSmooth = this.dinamicObjectSmooth;
    }

    public override void OnStartServer()
    {
        this.map = new MapGenerator(rowCount, colCount, countOfBreakWall, countOfEnemies, countOfProEnemies);
        this.map.AddGround(groundPrefab);
        this.map.AddUnbreakWall(unbreakWallPrefab);
        this.map.AddBreakWall(breakWallPrefab, randomBreakWall);
        //map.AddPlayer(playerPrefab);
        this.map.AddEnemy(enemyPrefab);
        this.map.AddProEnemy(enemyProPrefab);
        this.map.AddPowerUp(powerUpPrefab);
        this.map.AddGUI(GUI);
    }

    public override void OnStartClient()
    {
        this.map = new MapGenerator(rowCount, colCount, countOfBreakWall, countOfEnemies, countOfProEnemies);

        SetMatrixMap();

    }

    private void SetMatrixMap()
    {
        foreach (var obj in NetworkServer.objects)
        {
            switch (obj.Value.tag)
            {
                case "Ground":
                    AddObjectInMatrixMap(obj.Value.transform.position, ObjectType.Empty);
                    break;
                case "Break Wall":
                    AddObjectInMatrixMap(obj.Value.transform.position, ObjectType.BreakWall);
                    break;
                case "Unbreak Wall":
                    AddObjectInMatrixMap(obj.Value.transform.position, ObjectType.UnbreakWall);
                    break;
                case "Player":
                    AddObjectInMatrixMap(obj.Value.transform.position, ObjectType.Player);
                    break;
                case "Enemy":
                    AddObjectInMatrixMap(obj.Value.transform.position, ObjectType.Enemy);
                    break;
            }
        }
    }

    private void AddObjectInMatrixMap(Vector3 position, ObjectType objectType)
    {
        Game.matrixMap[(int)position.z, (int)position.x] = (int)objectType;
    }

}
