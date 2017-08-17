using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DinamicObjectController : MonoBehaviour
{
    protected bool canMove = true;
    public float Speed { get; set; }
    public abstract void Move();

    protected float Rotation { get; set; }

    protected void SetMove(Rigidbody rigidBody, float x, float y, float z)
    {
        rigidBody.transform.rotation = Quaternion.Euler(0, y, 0);
        Vector3 start = rigidBody.position;
        Vector3 end = rigidBody.position + new Vector3(x, 0, z) * Time.deltaTime * this.Speed;
        StartCoroutine(GetStep(rigidBody, start, end));
    }

    protected IEnumerator GetStep(Rigidbody rigidBody, Vector3 start, Vector3 end)
    {
        Vector3 diff = end - start;
        for (int i = 0; i < Game.DinamicObjectSmooth; i++)
        {
            rigidBody.transform.position += (diff / Game.DinamicObjectSmooth);
            yield return new WaitForEndOfFrame();
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

    protected IEnumerator SetTimeout(float seconds)
    {
        this.canMove = false;
        yield return new WaitForSeconds(seconds);
        this.canMove = true;
    }

}
