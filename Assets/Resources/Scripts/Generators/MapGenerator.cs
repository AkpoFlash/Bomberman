using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator {

    public MapGenerator(int rowCount, int colCount, int countOfBreakWall, int countOfEnemies, int countOfProEnemies)
    {
        Game.row = rowCount;
        Game.col = colCount;
        Game.CountOfBreakWall = countOfBreakWall;
        Game.CountOfEnemies = countOfEnemies;
        Game.CountOfProEnemies = countOfProEnemies;
        Game.matrixMap = new int[rowCount, colCount];
    }

    public void AddGround(GameObject gameObject)
    {
        GroundGenerator ground = new GroundGenerator();
        ground.Generate(gameObject);
    }

    public void AddBreakWall(GameObject gameObject, bool randomBreakWall = true)
    {
        if (randomBreakWall)
        {
            BreakWallRandomGenerator breakWall = new BreakWallRandomGenerator();
            breakWall.Generate(gameObject);
        }
        else
        {
            BreakWallGenerator breakWall = new BreakWallGenerator();
            breakWall.Generate(gameObject);
        }
            
    }

    public void AddUnbreakWall(GameObject gameObject)
    {
        UnbreakWallGenerator unbreakWall = new UnbreakWallGenerator();
        unbreakWall.Generate(gameObject);
    }

    public void AddPlayer(GameObject gameObject)
    {
        PlayerGenerator player = new PlayerGenerator();
        player.Generate(gameObject);
    }

    public void AddEnemy(GameObject gameObject)
    {
        EnemyGenerator enemy = new EnemyGenerator();
        enemy.Generate(gameObject);
    }

    public void AddProEnemy(GameObject gameObject)
    {
        EnemyProGenerator enemyPro = new EnemyProGenerator();
        enemyPro.Generate(gameObject);
    }

    public void AddPowerUp(GameObject[] gameObject)
    {
        PowerUpGenerator powerUp = new PowerUpGenerator();
        powerUp.Generate(gameObject);
    }

    public void AddGUI(GameObject gameObject)
    {
        GUIGenerator gui = new GUIGenerator();
        gui.Generate(gameObject);
    }

}
