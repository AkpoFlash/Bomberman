using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProController : DinamicObjectController
{
    private Rigidbody enemyRigidbody;
    private Animator animator;
    private Vector3 step;

    public override void Move()
    {
        this.animator.Play("Run");
        this.SetMove(enemyRigidbody, step.x, this.RotationByY(step.x, step.z), step.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //this.animator.Play("Attack");
            //Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.tag != "Ground")
        //{
        //    Vector3 nextPosition = Round(gameObject.transform.position + step);
        //    Vector3 collisionObjectPosition = Round(collision.gameObject.transform.position);

        //    if (CanStepForward(collisionObjectPosition, nextPosition))
        //    {
        //        //EndWalk();
        //    }
        //}
    }

    private void Start()
    {
        this.enemyRigidbody = gameObject.GetComponent<Rigidbody>();

        this.Speed = Game.DinamicObjectSpeed;
        this.animator = GetComponent<Animator>();
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

        Vector3 currentPosition = Round(gameObject.transform.position);
        Vector3 endPosition = Round(GameObject.FindGameObjectWithTag("Player").transform.position);

        List<Vector3> path = AStar.FindPath(pathMatrix, currentPosition, endPosition);

        if (path != null && path.Count > 0)
        {
            this.step = path.ToArray()[1] - currentPosition;
            this.Move();
        }
    }

    private bool IsNoWalkObject(int row, int col)
    {
        return Game.MatrixMap[row, col] == (int)ObjectType.UnbreakWall
            || Game.MatrixMap[row, col] == (int)ObjectType.BreakWall
            || Game.MatrixMap[row, col] == (int)ObjectType.Bomb;
    }

}

