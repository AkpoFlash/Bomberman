using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DinamicObjectGenerator  {

    protected virtual int CountOfObjects { get { return 1; } }

    public virtual void Generate(GameObject gameObject)
    {
        System.Random randomValue = new System.Random();
        int currentCountOfObject = 0;


        while (currentCountOfObject < this.CountOfObjects)
        {
            int row = randomValue.Next(1, Game.row);
            int col = randomValue.Next(1, Game.col);

            if (IsCellAvailable(row, col))
            {
                Game.AddObjectToMap(gameObject, new Vector3(col, 1, row), ObjectType.Enemy);
                currentCountOfObject++;
            }
        }
    }

    protected virtual bool IsCellAvailable(int row, int col)
    {
        return Game.matrixMap[row, col] == (int)ObjectType.Empty;
    }

}
