using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DinamicObjectController
{
    public GameObject bombPrefab;
    public int maxCountOfBombs = 1;

    private Rigidbody PlayerRigidbody { get; set; }

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

        this.SetMove(this.PlayerRigidbody, moveX, this.RotationByY(moveX, moveZ), moveZ);
    }

    private void Start()
    {
        this.Speed = Game.DinamicObjectSpeed;
        this.PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        this.Move();

        this.RemoveExplodedBombs();

        if (Input.GetKeyDown("space"))
        {
            if (CanPutBomb())
            {
                PutBomb();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
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

    private void PutBomb()
    {
        float x = Mathf.Round(gameObject.transform.position.x);
        float z = Mathf.Round(gameObject.transform.position.z);

        this.playersBomb.Add(Game.AddObjectToMap(this.bombPrefab, new Vector3(x, 0.5f, z), ObjectType.Bomb));
    }

    private bool CanPutBomb()
    {
        return playersBomb.Count < this.maxCountOfBombs;
    }

}
