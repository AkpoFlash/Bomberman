using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DinamicObjectController
{
    public int countOfNoRandomSteps = 50;

    private Rigidbody enemyRigidbody;
    private Vector3 step = new Vector3();
    private System.Random randomValue;
    private int currentCountOfSteps;
    private Animator animator;

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
        if (collision.gameObject.tag != "Ground")
        {
            Vector3 nextPosition = Round(gameObject.transform.position + step);
            Vector3 collisionObjectPosition = Round(collision.gameObject.transform.position);

            if (CanStepForward(collisionObjectPosition, nextPosition))
            {
                EndWalk();
            }
        }
    }

    private void Start()
    {
        this.enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        this.currentCountOfSteps = this.countOfNoRandomSteps;

        this.Speed = Game.DinamicObjectSpeed;
        this.randomValue = new System.Random();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        this.Move();
    }

    private void EndWalk()
    {
        this.currentCountOfSteps = this.countOfNoRandomSteps;
    }

    private bool CanStepForward(Vector3 collisionObjectPosition, Vector3 nextPosition)
    {
        return collisionObjectPosition.x == nextPosition.x && collisionObjectPosition.z == nextPosition.z;
    }

    private Vector3 GetRandomStep()
    {
        Vector3 step = new Vector3();

        switch (randomValue.Next(0,4))
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
