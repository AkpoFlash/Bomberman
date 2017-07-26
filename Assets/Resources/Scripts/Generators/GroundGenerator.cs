using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : EnvironmentGenerator {

    public override void Generate(GameObject gameObject)
    {
        for (int i = 0; i < Game.row; i++)
        {
            for (int j = 0; j < Game.col; j++)
            {
                Game.AddObjectToMap(gameObject, new Vector3(j, 0, i), ObjectType.Empty);
            }
        }
    }

}
