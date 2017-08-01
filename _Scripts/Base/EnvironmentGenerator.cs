using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnvironmentGenerator {

    abstract public void Generate(GameObject gameObject);

    protected bool IsBounded(int i, int j)
    {
        return i == 0 || j == 0 || i == Game.Row - 1 || j == Game.Col - 1;
    }

    protected bool IsEvenCell(int i, int j)
    {
        return !(i % 2 != 0 || j % 2 != 0);
    }

}
