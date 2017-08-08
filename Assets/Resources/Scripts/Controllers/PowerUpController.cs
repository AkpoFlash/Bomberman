using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
