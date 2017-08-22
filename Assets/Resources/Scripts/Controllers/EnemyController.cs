using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyController : DinamicObjectController
{
    public AudioClip soundOfAttack;
    public AudioClip soundOfWin;
    public int countOfNoRandomSteps = 50;

    protected Vector3 step = new Vector3();
    protected int currentCountOfSteps;
    protected Animator animator;
    protected System.Random randomValue;

    [Command]
    public override void CmdMove()
    {
        this.SetMove(this.step.x, this.RotationByY(this.step.x, this.step.z), this.step.z);
    }

    protected void PlayAttackSound()
    {
        audioEffect.clip = soundOfAttack;
        audioEffect.Play();
    }

    protected void PlayWinSound()
    {
        audioEffect.clip = soundOfWin;
        audioEffect.Play();
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

            this.Rigidbody.transform.position = Round(Rigidbody.position);
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
            this.animator.SetTrigger("Attack");
            StartCoroutine(SetMoveTimeout(5f));
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
        this.Rigidbody = gameObject.GetComponent<Rigidbody>();
        this.currentCountOfSteps = this.countOfNoRandomSteps;
        this.audioEffect = gameObject.GetComponentInChildren<AudioSource>();
        this.Speed = Game.DinamicObjectSpeed;
        this.randomValue = new System.Random();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        this.RandomStep();

        if (this.canMove)
            this.CmdMove();
    }

}
