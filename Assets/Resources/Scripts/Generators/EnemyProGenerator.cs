using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProGenerator : EnemyGenerator
{

    protected override int CountOfObjects { get { return Game.CountOfProEnemies; }}

}
