using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DinamicObjectController
{
    public int countOfNoRandomSteps = 50;

    protected Rigidbody enemyRigidbody;
    protected Vector3 step = new Vector3();
    protected int currentCountOfSteps;
    protected Animator animator;
    protected System.Random randomValue;

    public override void Move()
    {
        this.animator.Play("Run");
        this.SetMove(this.enemyRigidbody, this.step.x, this.RotationByY(this.step.x, this.step.z), this.step.z);
    }

    protected void RandomStep()
    {
        if (this.currentCountOfSteps < this.countOfNoRandomSteps)
        {
            this.currentCountOfSteps++;
        }
        else
        {
            this.currentCountOfSteps = 0;

            this.enemyRigidbody.transform.position = Round(enemyRigidbody.position);
            this.step = GetRandomStep();
        }
    }

    protected void EndMove()
    {
        this.currentCountOfSteps = this.countOfNoRandomSteps;
    }

    protected bool CanStepForward(Vector3 collisionObjectPosition, Vector3 nextPosition)
    {
        return collisionObjectPosition.x == nextPosition.x && collisionObjectPosition.z == nextPosition.z;
    }

    protected Vector3 GetRandomStep()
    {
        Vector3 step = new Vector3();

        switch (this.randomValue.Next(0,4))
        {
            case 0:
                step = this.Step(1,0);
                break;
            case 1:
                step = this.Step(-1,0);
                break;
            case 2:
                step = this.Step(0,-1);
                break;
            case 3:
                step = this.Step(0,1);
                break;
        }

        return step;
    }

    protected Vector3 Step(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //this.animator.Play("Attack");
            //Destroy(collision.gameObject);
        }
    }

    protected void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            Vector3 nextPosition = Round(gameObject.transform.position + step);
            Vector3 collisionObjectPosition = Round(collision.gameObject.transform.position);

            if (this.CanStepForward(collisionObjectPosition, nextPosition))
            {
                this.EndMove();
            }
        }
    }

    protected void Start()
    {
        this.enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        this.currentCountOfSteps = this.countOfNoRandomSteps;

        this.Speed = Game.DinamicObjectSpeed;
        this.randomValue = new System.Random();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        this.RandomStep();
        this.Move();
    }

}
