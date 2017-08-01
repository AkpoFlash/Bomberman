using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DinamicObjectController
{
    public GameObject bombPrefab;

    public int countOfBombs = 1;

    private Rigidbody PlayerRigidbody { get; set; }

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

        this.SetMove(this.PlayerRigidbody, moveX, this.RotationByY(moveX, moveZ), moveZ, Game.DinamicObjectSmooth);
    }

    void Start()
    {
        this.Speed = Game.DinamicObjectSpeed;
        this.PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        this.Move();

        if (Input.GetKeyDown("space"))
        {
            float x = Mathf.Round(gameObject.transform.position.x);
            float z = Mathf.Round(gameObject.transform.position.z);

            Game.AddObjectToMap(bombPrefab, new Vector3(x, 0.5f, z), ObjectType.Bomb);
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.isTrigger = false;
    }

}
