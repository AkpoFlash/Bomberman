using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    public void CreateMap()
    {
        this.GenerateGround();
        this.GenerateConcretWalls();
        this.GenerateBreakWalls();
    }

    private void GenerateGround()
    {
        for (int i = 0; i < Map.row; i++)
        {
            for (int j = 0; j < Map.col; j++)
            {
                Instantiate(Environment.GetGround(), new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }

    private void GenerateConcretWalls()
    {
        for (int i = 0; i < Map.row; i++)
        {
            for (int j = 0; j < Map.col; j++)
            {
                if (IsBounded(i,j) || IsEvenCell(i,j))
                {
                    Instantiate(Environment.GetConcretWall(), new Vector3(i, 0.5f, j), Quaternion.identity);
                    Map.matrixMap[i,j] = 1;
                }
            }
        }
    }

    private void GenerateBreakWalls()
    {
        System.Random rand = new System.Random();
        int countOfWall = 0;

        for (int i = 0; i < Map.row; i++)
        {
            for (int j = 0; j < Map.col; j++)
            {
                if(Map.matrixMap[i, j] == 0 && countOfWall < Map.countOfBreakWall && rand.NextDouble() > 0.75) {
                    Instantiate(Environment.GetBreakWall(), new Vector3(i, 0.5f, j), Quaternion.identity);
                    countOfWall++;
                }
            }
        }
    }

    private bool IsBounded(int i, int j)
    {
        return i == 0 || j == 0 || i == Map.row - 1 || j == Map.col - 1;
    }

    private bool IsEvenCell(int i, int j)
    {
        return !(i % 2 != 0 || j % 2 != 0);
    }
        

}
