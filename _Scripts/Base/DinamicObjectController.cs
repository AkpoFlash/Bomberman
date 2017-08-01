using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DinamicObjectController : MonoBehaviour  {

    public float Speed { get; set; }

    protected float Rotation { get; set; }

    abstract public void Move();

    protected void SetMove(Rigidbody rigidBody, float x, float y, float z, int countOfstep)
    {
        rigidBody.transform.rotation = Quaternion.Euler(0, y, 0);
        Vector3 start = rigidBody.position;
        Vector3 end = rigidBody.position + new Vector3(x, 0, z) * Time.deltaTime * this.Speed;
        StartCoroutine(GetStep(rigidBody, start, end, countOfstep));
    }

    protected IEnumerator GetStep(Rigidbody rigidBody, Vector3 start, Vector3 end, int steps)
    {
        Vector3 diff = end - start;
        for (int i = 0; i < steps; i++)
        {
            rigidBody.transform.position += (diff / steps);
            yield return new WaitForEndOfFrame();
        }
    }

    protected bool IsStopMove(float x, float z)
    {
        if (x == 0 && z == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected float RotationByY(float horizontal, float vertical)
    {
        if (horizontal == 0 && vertical == 0)
        {
            return this.Rotation;
        }

        if (horizontal == 0)
        {
            return this.Rotation = (vertical < 0) ? 180 : 0;
        }

        if (horizontal < 0)
        {
            return this.Rotation = 270;
        }

        if (horizontal > 0)
        {
            return this.Rotation = 90;
        }

        return this.Rotation = 0;
    }

    protected Vector3 Round(Vector3 vector)
    {
        return new Vector3(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }

    protected Vector3 Invert(Vector3 vector)
    {
        return new Vector3(-vector.x, -vector.y, -vector.z);
    }

}
