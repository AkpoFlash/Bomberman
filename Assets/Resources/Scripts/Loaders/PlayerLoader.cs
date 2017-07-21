using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : Player {

    public override GameObject GetElement(string name)
    {
        return Resources.Load("Prefabs/Players/" + name) as GameObject;
    }

}
