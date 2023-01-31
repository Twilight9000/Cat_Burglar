using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject door;
    public float speed = 2;
    public bool move;

    public Transform newPosition;

    void Start()
    {
        move = false;
    }

    void Update()
    {
        if(move)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, newPosition.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            move = true;
        }
    }
}
