using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DinamicObjectController
{
    public GameObject bombPrefab;
    public int maxCountOfBombs = 1;
    public int countOfExplosions = 1;
    public bool wallHack = false;

    private Rigidbody PlayerRigidbody { get; set; }
    private Animator animator;
    private List<GameObject> playersBomb = new List<GameObject>();

    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float moveX = 0;
        float moveZ = 0;

        if(Mathf.Abs(x) > Mathf.Abs(z))
            moveX = x;
        else
            moveZ = z;

        if (moveX == 0 && moveZ == 0)
        {
            animator.Play("Stand");
        }
        else
        {
            animator.Play("Run");
        }

        this.SetMove(this.PlayerRigidbody, moveX, this.RotationByY(moveX, moveZ), moveZ);
    }

    private void Start()
    {
        this.Speed = Game.DinamicObjectSpeed;
        this.PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        this.Move();

        this.RemoveExplodedBombs();

        if (Input.GetKeyDown("space"))
        {
            float x = Mathf.Round(gameObject.transform.position.x);
            float z = Mathf.Round(gameObject.transform.position.z);

            if (CanPutBomb(x,z))
            {
                PutBomb(x,z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Bomb":
                other.isTrigger = false;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int row = (int)other.gameObject.transform.position.z;
        int col = (int)other.gameObject.transform.position.x;
        if (Game.MatrixMap[row, col] != (int)ObjectType.BreakWall)
        {
            switch (other.gameObject.tag)
            {
                case "BombPowerUp":
                    maxCountOfBombs++;
                    Destroy(other.gameObject);
                    break;
                case "ExplosionPowerUp":
                    this.countOfExplosions++;
                    Destroy(other.gameObject);
                    break;
                case "SpeedPowerUp":
                    this.Speed++;
                    Destroy(other.gameObject);
                    break;
                case "WallHackPowerUp":
                    if (!wallHack)
                    {
                        foreach (GameObject breakWall in GameObject.FindGameObjectsWithTag("Break Wall"))
                        {
                            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), breakWall.GetComponent<Collider>());
                        }
                        wallHack = true;
                    }
                    Destroy(other.gameObject);
                    break;
            }
        }
    }

    private void RemoveExplodedBombs()
    {
        foreach (var bomb in this.playersBomb.ToArray())
        {
            if (bomb == null)
            {
                this.playersBomb.Remove(bomb);
            }
        }
    }

    private void PutBomb(float x, float z)
    {
        GameObject bomb = Game.AddObjectToMap(this.bombPrefab, new Vector3(x, 0.5f, z), ObjectType.Bomb);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.countOfExplosions = this.countOfExplosions;

        this.playersBomb.Add(bomb);
    }

    private bool CanPutBomb(float x, float z)
    {
        return Game.MatrixMap[(int)z, (int)x] != (int)ObjectType.BreakWall
            && Game.MatrixMap[(int)z, (int)x] != (int)ObjectType.Bomb
            && playersBomb.Count < this.maxCountOfBombs;
    }

}
