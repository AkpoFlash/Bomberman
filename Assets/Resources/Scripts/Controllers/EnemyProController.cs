using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProController : EnemyController
{

    protected bool IsNoWalkObject(int row, int col)
    {
        return Game.MatrixMap[row, col] == (int)ObjectType.UnbreakWall
            || Game.MatrixMap[row, col] == (int)ObjectType.BreakWall
            || Game.MatrixMap[row, col] == (int)ObjectType.Bomb;
    }

    private void FixedUpdate()
    {
        int[,] pathMatrix = new int[Game.Row, Game.Col];

        for (int i = 0; i < Game.Row; i++)
        {
            for (int j = 0; j < Game.Col; j++)
            {
                if (IsNoWalkObject(i, j))
                {
                    pathMatrix[i, j] = 1;
                }
            }
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Vector3 currentPosition = Round(gameObject.transform.position);
            Vector3 endPosition = Round(GameObject.FindGameObjectWithTag("Player").transform.position);

            List<Vector3> path = AStar.FindPath(pathMatrix, currentPosition, endPosition);


            if (path != null && path.Count > 0)
            {
                this.step = path.ToArray()[1] - currentPosition;
            }
            else
            {
                this.RandomStep();
            }
        }
        else
        {
            this.RandomStep();
        }


        if (this.canMove)
            this.Move();

    }

}

