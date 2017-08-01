using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public int countOfExplosions = 2;

    public float secondsToDisappear = 0.3f;

    public float secondsToStep = 0.1f;

    void Start ()
    {
        StartCoroutine(SetTimeout());
    }

    void FixedUpdate ()
    {
		
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

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Unbreak Wall")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
