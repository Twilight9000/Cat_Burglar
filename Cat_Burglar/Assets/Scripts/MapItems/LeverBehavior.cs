using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public IInteractable activate;

    public float minWeight = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Rigidbody>() && collision.collider.GetComponent<Rigidbody>().mass >= minWeight)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            if (activate != null)
            {
                activate.Interact();
            }
        }
    }
}
