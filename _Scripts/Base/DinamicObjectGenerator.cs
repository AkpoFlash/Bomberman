using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DinamicObjectGenerator  {

    protected virtual int CountOfObjects { get { return 1; } }

    protected abstract ObjectType objectType { get; }

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
                Game.AddObjectToMap(gameObject, new Vector3(col, 1, row), objectType);
                currentCountOfObject++;
            }
        }

    }

    protected virtual bool IsCellAvailable(int row, int col)
    {
        return Game.MatrixMap[row, col] == (int)ObjectType.Empty;
    }

}
