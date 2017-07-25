﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : DinamicObject {

    public override GameObject GetElement(string name)
    {
        return Resources.Load("Prefabs/DinamicObject/" + name) as GameObject;
    }

}
