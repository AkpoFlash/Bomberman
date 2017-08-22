using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpController : NetworkBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
