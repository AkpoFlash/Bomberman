using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {

    public MapGenerator(int rowCount, int colCount, int countOfBreakWall, int countOfEnemies)
    {
        Map.row = rowCount;
        Map.col = colCount;
        Map.countOfBreakWall = countOfBreakWall;
        Map.countOfEnemies = countOfEnemies;
        Map.matrixMap = new int[rowCount, colCount];
    }

    public void AddGround(GameObject gameObject)
    {
        GroundGenerator ground = new GroundGenerator();
        ground.Generate(gameObject.name);
    }

    public void AddBreakWall(GameObject gameObject, bool randomBreakWall = true)
    {
        BreakWallGenerator breakWall = new BreakWallGenerator();

        if(randomBreakWall)
            breakWall.GenerateRandom(gameObject.name);
        else
            breakWall.Generate(gameObject.name);
    }

    public void AddUnbreakWall(GameObject gameObject)
    {
        UnbreakWallGenerator unbreakWall = new UnbreakWallGenerator();
        unbreakWall.Generate(gameObject.name);
    }

    public void AddPlayer(GameObject gameObject)
    {
        PlayerGenerator player = new PlayerGenerator();
        player.Generate(gameObject.name);
    }

    public void AddEnemy(GameObject gameObject)
    {
        EnemyGenerator enemy = new EnemyGenerator();
        enemy.Generate(gameObject.name);
    }

}
