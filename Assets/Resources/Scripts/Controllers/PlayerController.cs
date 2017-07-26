using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DinamicObjectController {

    public float Speed { get; set; }
    private Rigidbody PlayerRigidbody { get; set; }

    public PlayerController(GameObject playerObject, float speed)
    {
        this.PlayerRigidbody = GameObject.FindWithTag(playerObject.tag).GetComponent<Rigidbody>();
        this.Speed = speed;
    }

    public override void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        PlayerRigidbody.transform.rotation = Quaternion.Euler(0, GetRotationByY(x, z), 0);
        PlayerRigidbody.transform.position += new Vector3(x, 0, z) * Time.deltaTime * this.Speed;
    }

}
