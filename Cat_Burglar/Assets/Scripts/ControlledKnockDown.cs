using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledKnockDown : MonoBehaviour
{
    Rigidbody rb;
    Vector3 force;
    bool isInAir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            force = transform.position - collision.gameObject.transform.position;
            rb.AddForce(force.normalized * 2, ForceMode.Impulse);
        }
        else
        {
            if(isInAir)
            {
                isInAir = false;
                StartCoroutine(WaitForToTurnStatic());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        {
            isInAir = true;
        }
    }

    IEnumerator WaitForToTurnStatic()
    {
        yield return new WaitForSeconds(1f);
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
