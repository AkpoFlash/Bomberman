using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DinamicObjectGenerator
{

    protected virtual int CountOfObjects { get { return 1; } }

    protected abstract ObjectType TypeOfObject { get; }

    public virtual void Generate(GameObject gameObject)
    {
        System.Random randomValue = new System.Random();
        int currentCountOfObject = 0;

        while (currentCountOfObject < this.CountOfObjects)
        {
            int row = randomValue.Next(1, Game.Row);
            int col = randomValue.Next(1, Game.Col);

            if (IsCellAvailable(row, col))
            {
                Game.AddObjectToMap(gameObject, new Vector3(col, 0, row), TypeOfObject);
                currentCountOfObject++;
            }
        }

    }

    protected virtual bool IsCellAvailable(int row, int col)
    {
        return Game.MatrixMap[row, col] == (int)ObjectType.Empty;
    }

}
