using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Environment {

    public static GameObject GetBreakWall()
    {
        return Resources.Load("Prefabs/Environment/BreakWall") as GameObject;
    }

    public static GameObject GetConcretWall()
    {
        return Resources.Load("Prefabs/Environment/ConcretWall") as GameObject;
    }

    public static GameObject GetGround()
    {
        return Resources.Load("Prefabs/Environment/Ground") as GameObject;
    }

}
