using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class DinamicObjectController : NetworkBehaviour
{
    public AudioClip soundOfStep;
    public AudioClip soundOfDeath;
    public float Speed { get; set; }
    public abstract void CmdMove();

    protected Rigidbody Rigidbody { get; set; }
    protected float Rotation { get; set; }
    protected bool canMove = true;
    protected AudioSource audioEffect;

    protected void SetMove(float x, float y, float z)
    {
        this.Rigidbody.transform.rotation = Quaternion.Euler(0, y, 0);
        Vector3 start = this.Rigidbody.position;
        Vector3 end = this.Rigidbody.position + new Vector3(x, 0, z) * Time.deltaTime * this.Speed;
        StartCoroutine(GetStep(this.Rigidbody, start, end));
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

    protected IEnumerator SetMoveTimeout(float seconds)
    {
        this.canMove = false;
        yield return new WaitForSeconds(seconds);
        this.canMove = true;
    }

    protected void PlayStepSound()
    {
        audioEffect.clip = soundOfStep;
        audioEffect.Play();
    }

    protected void PlayDeathSound()
    {
        audioEffect.clip = soundOfDeath;
        audioEffect.Play();
    }

}
