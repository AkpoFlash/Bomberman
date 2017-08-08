using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIGenerator
{

    public void Generate(GameObject gameObject)
    {
        Game.GUI = MonoBehaviour.Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity);

    }

}
