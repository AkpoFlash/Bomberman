using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : DinamicObjectGenerator {

    public GameObject GameObject { get; set; }

    public override void Generate(GameObject gameObject)
    {
        System.Random randomValue = new System.Random();

        while (true)
        {
            int row = randomValue.Next(1, Game.row);
            int col = randomValue.Next(1, Game.col);

            if (Game.matrixMap[row, col] == (int)ObjectType.Empty && CanSetPlayer(row, col))
            {
                Game.AddObjectToMap(gameObject, new Vector3(col, 1, row), ObjectType.Player);
                break;
            }
        }
    }

    private bool CanSetPlayer(int row, int col)
    {
        bool emptyUp = Game.matrixMap[row + 1, col] == (int)ObjectType.Empty;
        bool emptyRight = Game.matrixMap[row, col + 1] == (int)ObjectType.Empty;
        bool emptyDown = Game.matrixMap[row - 1, col] == (int)ObjectType.Empty;
        bool emptyLeft = Game.matrixMap[row, col - 1] == (int)ObjectType.Empty;

        return (emptyUp && emptyRight) || (emptyRight && emptyDown) || (emptyDown && emptyLeft) || (emptyLeft && emptyUp);
    }

}
