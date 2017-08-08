using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentGenerator
{

    protected abstract int StartCoordinate { get; }
    protected abstract int MaxRowCoordinate { get; }
    protected abstract int MaxColCoordinate { get; }
    protected abstract float PositionOnY { get; }

    protected abstract ObjectType TypeOfObject { get; }

    public virtual void Generate(GameObject gameObject)
    {
        for (int i = this.StartCoordinate; i < this.MaxRowCoordinate; i++)
        {
            for (int j = this.StartCoordinate; j < this.MaxColCoordinate; j++)
            {
                if (this.IsCellAvailable(i, j))
                {
                    Game.AddObjectToMap(gameObject, new Vector3(j, this.PositionOnY, i), this.TypeOfObject);
                }
            }
        }
    }

    protected abstract bool IsCellAvailable(int row, int col);

    protected bool IsBounded(int i, int j)
    {
        return i == 0 || j == 0 || i == Game.Row - 1 || j == Game.Col - 1;
    }

    protected bool IsEvenCell(int i, int j)
    {
        return !(i % 2 != 0 || j % 2 != 0);
    }

}
