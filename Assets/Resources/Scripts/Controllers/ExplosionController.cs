using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    public int countOfExplosions = 2;

    public float secondsToDisappear = 0.3f;

    private void Start ()
    {
        StartCoroutine(SetTimeout());
    }

    IEnumerator SetTimeout()
    {
        yield return new WaitForSeconds(this.secondsToDisappear);
        MakeStep();
    }

    private void MakeStep()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Destroy(other.gameObject);
    }

}
