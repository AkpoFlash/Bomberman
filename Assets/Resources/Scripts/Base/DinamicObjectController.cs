using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DinamicObjectController  {

    abstract public void Move();

    protected float GetRotationByY(float horizontal, float vertical)
    {
        if (horizontal == 0)
        {
            return (vertical < 0) ? 180 : 0;
        }

        if (horizontal < 0)
        {
            return (vertical < 0) ? 225 : (vertical > 0) ? 315 : 270;
        }

        if (horizontal > 0)
        {
            return (vertical < 0) ? 135 : (vertical > 0) ? 45 : 90;
        }

        return 0;
    }

}
