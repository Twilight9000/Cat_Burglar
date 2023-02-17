using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    public GameObject cat;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        cat = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();

    }

    public void Update()
    {

        //would go under cat behaviour propably
        if (Input.GetMouseButtonDown(1) && transform.parent == cat.transform.parent)
        {

            transform.localPosition = new Vector3(0, 0.219f, 0.35f);

            transform.SetParent(null);

            gameObject.GetComponent<SphereCollider>().enabled = true;

            rb.constraints = RigidbodyConstraints.None;

            rb.AddRelativeForce(new Vector3(0, 200, 1000));

        }

    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == cat)
        {

            gameObject.GetComponent<SphereCollider>().enabled = false;
            transform.SetParent(cat.transform.parent);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.localPosition = new Vector3(0, 0.219f, 0.2975f);
            transform.rotation = new Quaternion(0, 0, 0, 0);

        }
    }

}
