using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Cat")
        {
            Destroy(door);
        }
    }
}
