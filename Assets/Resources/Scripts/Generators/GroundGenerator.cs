using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : EnvironmentGenerator {

    public void Generate(string prefabName)
    {
        GroundLoader groundPrefab = new GroundLoader();

        for (int i = 0; i < Map.row; i++)
        {
            for (int j = 0; j < Map.col; j++)
            {
                Instantiate(groundPrefab.GetElement(prefabName), new Vector3(j, 0, i), Quaternion.identity);
            }
        }
    }

}
