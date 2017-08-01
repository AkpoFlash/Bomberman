using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : DinamicObjectGenerator {

    protected override int CountOfObjects { get { return Game.CountOfEnemies; }}

    protected override ObjectType TypeOfObject { get { return ObjectType.Enemy; } }

}
