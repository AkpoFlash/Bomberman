using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

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
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            other.GetComponentInParent<Animator>().SetTrigger("Killed");
            other.transform.position += new Vector3(0, -0.75f, 0);
            other.GetComponentInParent<DinamicObjectController>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject, 4);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
