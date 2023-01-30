using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBehaviour : MonoBehaviour
{
    public Rigidbody myRB;
    public bool isStolen = false;

    // Start is called before the first frame update
    void Start()
    {
        myRB = gameObject.GetComponent<Rigidbody>();
        isStolen = false;
    }

    private void Update()
    {
        Debug.Log(isStolen);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision...");
            isStolen = true;
            gameObject.SetActive(false);
        }
    }
}
