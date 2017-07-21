using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    public static GameObject GetSimpleEnemy()
    {
        return Resources.Load("Prefabs/Enemies/SimpleEnemy") as GameObject;
    }

}
