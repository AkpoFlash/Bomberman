using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DinamicObjectController
{
    private Rigidbody enemyRigidbody;

    private System.Random RandomValue;

    private int currentCountOfSteps;
    private Vector3 step = new Vector3();
    private int countOfNoRandomSteps = 50;


    public override void Move()
    {
        if (currentCountOfSteps < countOfNoRandomSteps)
        {
            currentCountOfSteps++;
        }
        else
        {
            currentCountOfSteps = 0;

            enemyRigidbody.transform.position = Round(enemyRigidbody.position);
            step = GetRandomStep();
        }
        this.SetMove(enemyRigidbody, step.x, this.RotationByY(step.x, step.z), step.z, Game.DinamicObjectSmooth);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            Vector3 nextPosition = Round(gameObject.transform.position + step);
            Vector3 collisionObjectPosition = Round(collision.gameObject.transform.position);

            if (collisionObjectPosition.x == nextPosition.x
                && collisionObjectPosition.z == nextPosition.z)
            {
                this.currentCountOfSteps = this.countOfNoRandomSteps;
            }
        }
    }

    void Start()
    {
        this.enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        this.currentCountOfSteps = this.countOfNoRandomSteps;

        this.Speed = Game.DinamicObjectSpeed;
        this.RandomValue = new System.Random();
    }

    void FixedUpdate()
    {
        this.Move();
    }

    private Vector3 GetRandomStep()
    {
        Vector3 step = new Vector3();

        switch (RandomValue.Next(0,4))
        {
            case 0:
                step = Step(1,0);
                break;
            case 1:
                step = Step(-1,0);
                break;
            case 2:
                step = Step(0,-1);
                break;
            case 3:
                step = Step(0,1);
                break;
        }

        return step;
    }

    private Vector3 Step(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

}
