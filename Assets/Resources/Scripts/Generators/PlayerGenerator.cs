using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : DinamicObjectGenerator
{

    public GameObject GameObject { get; set; }

    protected override ObjectType TypeOfObject { get { return ObjectType.Player; } }

    protected override bool IsCellAvailable(int row, int col)
    {
        return base.IsCellAvailable(row, col) && CanSetPlayer(row, col);
    }

    private bool CanSetPlayer(int row, int col)
    {
        bool emptyUp = Game.MatrixMap[row + 1, col] == (int)ObjectType.Empty;
        bool emptyRight = Game.MatrixMap[row, col + 1] == (int)ObjectType.Empty;
        bool emptyDown = Game.MatrixMap[row - 1, col] == (int)ObjectType.Empty;
        bool emptyLeft = Game.MatrixMap[row, col - 1] == (int)ObjectType.Empty;

        return (emptyUp && emptyRight) || (emptyRight && emptyDown) || (emptyDown && emptyLeft) || (emptyLeft && emptyUp);
    }

}
