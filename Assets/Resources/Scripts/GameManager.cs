using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject mainCamera;

    public int rowCount = 15;
    public int colCount = 15;
    public int countOfBreakWall = 50;

    public GameObject ground;
    public GameObject breakWall;
    public GameObject unbreakWall;
    public GameObject player;
    public GameObject enemy;

    public bool randomBreakWall = true;


    void Start () {
        MapGenerator map = new MapGenerator(rowCount, colCount, countOfBreakWall);
        Camera camera = new Camera(mainCamera);
        map.AddGround(ground);
        map.AddUnbreakWall(unbreakWall);
        map.AddBreakWall(breakWall, randomBreakWall);
        map.AddPlayer(player);
        map.AddEnemy(enemy);
    }

}
