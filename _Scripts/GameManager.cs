using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject mainCamera;

    public int rowCount = 15;
    public int colCount = 15;
    public int countOfBreakWall = 50;
    public int countOfEnemies = 3;
    public int dinamicObjectSmooth = 20;
    public float dinamicObjectSpeed = 5;

    public GameObject groundPrefab;
    public GameObject breakWallPrefab;
    public GameObject unbreakWallPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public bool randomBreakWall = true;

    void Start() {
        MapGenerator map = new MapGenerator(rowCount, colCount, countOfBreakWall, countOfEnemies);
        Camera.SetCamera(mainCamera);

        map.AddGround(groundPrefab);
        map.AddUnbreakWall(unbreakWallPrefab);
        map.AddBreakWall(breakWallPrefab, randomBreakWall);
        map.AddPlayer(playerPrefab);
        map.AddEnemy(enemyPrefab);

        Game.DinamicObjectSpeed = this.dinamicObjectSpeed;
        Game.DinamicObjectSmooth = this.dinamicObjectSmooth;
    }

    void FixedUpdate()
    {

    }

}
