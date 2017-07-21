using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallLoader : Environment
{

    public override GameObject GetElement(string name)
    {
        return Resources.Load("Prefabs/Environment/" + name) as GameObject;
    }

}
