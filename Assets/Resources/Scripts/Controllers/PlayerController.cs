using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DinamicObjectController
{

    private Rigidbody PlayerRigidbody { get; set; }

    //public PlayerController(GameObject playerObject, float speed)
    //{
    //    this.PlayerRigidbody = GameObject.FindWithTag(playerObject.tag).GetComponent<Rigidbody>();
    //    this.Speed = speed;
    //}

    public override void Move()
    {
        this.PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
        this.Speed = 5;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        this.SetMove(PlayerRigidbody, x, GetRotationByY(x, z), z);
    }

    void FixedUpdate()
    {
        Move();
    }
}
