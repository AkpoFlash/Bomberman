using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public GameObject explosionPrefab;
    public int countOfExplosions;
    public float secondsToExplosion = 3;

    private Dictionary<Vector3, bool> explosionDirection = new Dictionary<Vector3, bool>();

    private void Start ()
    {
        StartCoroutine(SetTimeout());
    }

    IEnumerator SetTimeout()
    {
        yield return new WaitForSeconds(this.secondsToExplosion);
        StartCoroutine(MakeExplosion());
    }

    private IEnumerator MakeExplosion()
    {
        float x = Mathf.Round(gameObject.transform.position.x);
        float z = Mathf.Round(gameObject.transform.position.z);

        AudioSource audioEffect = gameObject.GetComponent<AudioSource>();
        audioEffect.Play();

        gameObject.transform.localScale = new Vector3(0,0,0);
        this.explosionDirection.Add(new Vector3(x, 0.5f, z), StepForward(new Vector3(x, 0.5f, z)));

        for (int i = 1; i <= this.countOfExplosions; i++)
        {
            ExplosionStep(new Vector3(x + i, 0.5f, z), new Vector3(x + i - 1, 0.5f, z));
            ExplosionStep(new Vector3(x - i, 0.5f, z), new Vector3(x - i + 1, 0.5f, z));
            ExplosionStep(new Vector3(x, 0.5f, z + i), new Vector3(x, 0.5f, z + i - 1));
            ExplosionStep(new Vector3(x, 0.5f, z - i), new Vector3(x, 0.5f, z - i + 1));

            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }

    private void ExplosionStep(Vector3 nextPosition, Vector3 prevPosition)
    {
        this.explosionDirection.Add(
                nextPosition,
                (explosionDirection[prevPosition]) ? this.StepForward(nextPosition) : false
                );
    }

    private bool StepForward(Vector3 position)
    {
        if (IsСoordinateInMap(position))
        {
            if (!IsObjectInPosition(position, ObjectType.UnbreakWall))
            {
                if (IsObjectInPosition(position, ObjectType.BreakWall))
                {
                    Game.AddObjectToMap(this.explosionPrefab, position, ObjectType.Explosion);
                    return false;
                }
                else
                {
                    Game.AddObjectToMap(this.explosionPrefab, position, ObjectType.Explosion);
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsСoordinateInMap(Vector3 position)
    {
        return position.x > 0 && position.z > 0 && position.x < Game.Col - 1 && position.z < Game.Row - 1;
    }

    private bool IsObjectInPosition(Vector3 position, ObjectType gameObjectType)
    {
        return Game.MatrixMap[(int)position.z, (int)position.x] == (int)gameObjectType;
    }

}
