using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject mainCamera;

    public int rowCount = 15;
    public int colCount = 15;
    public int countOfBreakWall = 50;
    public int countOfEnemies = 3;
    public float dinamicObjectSpeed = 5;

    public GameObject groundPrefab;
    public GameObject breakWallPrefab;
    public GameObject unbreakWallPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public bool randomBreakWall = true;

    private PlayerController playerCtrl;
    private EnemyController enemyCtrl;

    void Start() {
        MapGenerator map = new MapGenerator(rowCount, colCount, countOfBreakWall, countOfEnemies);
        Camera camera = new Camera(mainCamera);
        map.AddGround(groundPrefab);
        map.AddUnbreakWall(unbreakWallPrefab);
        map.AddBreakWall(breakWallPrefab, randomBreakWall);
        map.AddPlayer(playerPrefab);
        map.AddEnemy(enemyPrefab);

        playerCtrl = new PlayerController(playerPrefab, dinamicObjectSpeed);
        enemyCtrl = new EnemyController(enemyPrefab, dinamicObjectSpeed);
    }

    void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.2f;
        playerCtrl.Move();
        enemyCtrl.Move();
    }

}
