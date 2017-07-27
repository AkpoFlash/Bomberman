using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DinamicObjectController : MonoBehaviour  {

    public float Speed { get; set; }

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

    protected void SetMove(Rigidbody rigidBody, float x, float y, float z)
    {
        rigidBody.transform.rotation = Quaternion.Euler(0, y, 0);
        rigidBody.transform.position += new Vector3(x, 0, z) * Time.deltaTime * this.Speed;
    }

    protected void SetSmoothMove(Rigidbody rigidBody, float x, float y, float z)
    {
        rigidBody.transform.rotation = Quaternion.Euler(0, y, 0);
        Vector3 start = rigidBody.transform.position;
        Vector3 end = rigidBody.transform.position + new Vector3(x*10, 0, z*10) * Time.deltaTime * this.Speed;
        StartCoroutine(Step(rigidBody, start, end));
    }

    protected IEnumerator Step(Rigidbody rigidBody, Vector3 start, Vector3 end)
    {
        Vector3 diff = end - start;
        for (int i = 0; i < 10; i++)
        {
            rigidBody.transform.position += diff / 10;
            yield return new WaitForEndOfFrame();
        }
    }

}
