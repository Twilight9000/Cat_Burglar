using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject door;
    public float speed = 2;

    Vector3 currentPosition;
    Vector3 newPosition;

    void Start()
    {
        float x = door.transform.position.x;
        float y = door.transform.position.y + 10;
        float z = door.transform.position.z;

        currentPosition = door.transform.position;
        newPosition = (currentPosition);
        newPosition.y = newPosition.y + 10;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Cat")
        {
            door.transform.position = Vector3.MoveTowards(currentPosition, newPosition, speed);
        }
    }
}
