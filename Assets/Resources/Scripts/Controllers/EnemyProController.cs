using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyProController : EnemyController
{
    private int[,] pathMatrix = new int[Game.row, Game.col];


    protected bool IsNoWalkObject(int row, int col)
    {
        return Game.matrixMap[row, col] == (int)ObjectType.UnbreakWall
            || Game.matrixMap[row, col] == (int)ObjectType.BreakWall
            || Game.matrixMap[row, col] == (int)ObjectType.Bomb;
    }

    private void FixedUpdate()
    {
        CmdSetPathMatrix();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Vector3 currentPosition = Round(gameObject.transform.position);
            Vector3 endPosition = Round(GameObject.FindGameObjectWithTag("Player").transform.position);
             
            List<Vector3> path = AStar.CmdFindPath(pathMatrix, currentPosition, endPosition);


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
            this.CmdMove();

    }

    [Command]
    private void CmdSetPathMatrix()
    {
        for (int i = 0; i < Game.row; i++)
        {
            for (int j = 0; j < Game.col; j++)
            {
                if (IsNoWalkObject(i, j))
                {
                    pathMatrix[i, j] = 1;
                }
            }
        }
    }

}

