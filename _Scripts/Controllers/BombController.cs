using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public GameObject explosionPrefab;
    //public int countOfExplosions = 2;

    public float secondsToExplosion = 4;

	void Start ()
    {
        StartCoroutine(SetTimeout());
    }

    IEnumerator SetTimeout()
    {
        yield return new WaitForSeconds(this.secondsToExplosion);
        MakeExplosion();
    }

    private void MakeExplosion()
    {
        float x = Mathf.Round(gameObject.transform.position.x);
        float z = Mathf.Round(gameObject.transform.position.z);

        ExplosionStep(new Vector3(x, 0.5f, z));
        gameObject.transform.localScale = new Vector3(0, 0, 0);
      
        Destroy(gameObject);
    }

    private void ExplosionStep(Vector3 position)
    {
        Game.AddObjectToMap(explosionPrefab, position, ObjectType.Explosion);
    }

    //private IEnumerator MakeExplosion()
    //{
    //    float x = Mathf.Round(gameObject.transform.position.x);
    //    float z = Mathf.Round(gameObject.transform.position.z);

    //    ExplosionStep(new Vector3(x, 0.5f, z));
    //    gameObject.transform.localScale = new Vector3(0,0,0);

    //    for (int i = 1; i <= countOfExplosions; i++)
    //    {
    //        ExplosionStep(new Vector3(x + i, 0.5f, z));
    //        ExplosionStep(new Vector3(x - i, 0.5f, z));
    //        ExplosionStep(new Vector3(x, 0.5f, z + i));
    //        ExplosionStep(new Vector3(x, 0.5f, z - i));

    //        yield return new WaitForSeconds(0.1f);
    //    }

    //    Destroy(gameObject);
    //}

    //private void ExplosionStep(Vector3 position)
    //{
    //    Game.AddObjectToMap(explosionPrefab, position, ObjectType.Explosion);
    //}

}
