using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpGenerator
{

    private System.Random randomValue = new System.Random();

    public void Generate(GameObject[] gameObject)
    {

        for (int i = 1; i < Game.row - 1; i++)
        {
            for (int j = 1; j < Game.col - 1; j++)
            {
                if (this.IsBreakWall(i, j) && Game.CheckRandomAppearance(Game.probabilityAppearancePowerUp, this.randomValue))
                {
                    int currentPowerUp = GetRandomPowerUp(gameObject.Length);
                    GameObject obj = MonoBehaviour.Instantiate(gameObject[currentPowerUp], new Vector3(j, 0, i), Quaternion.identity);
                    NetworkServer.Spawn(obj);
                }
            }
        }
    }

    protected bool IsBreakWall(int row, int col)
    {
        return Game.matrixMap[row, col] == (int)ObjectType.BreakWall;
    }

    private int GetRandomPowerUp(int borderOfRandom)
    {
        int powerUp = 0;

        switch (randomValue.Next(0, borderOfRandom + 1))
        {
            case 0:
                powerUp = 0;
                break;
            case 1:
                powerUp = 1;
                break;
            case 2:
                powerUp = 2;
                break;
            case 3:
                powerUp = 3;
                break;
        }

        return powerUp;
    }

}
