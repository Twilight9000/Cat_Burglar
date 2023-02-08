using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorBehavior : MonoBehaviour, IInteractable
{
    public float speed = 2;
    public bool move;

    public Transform newPosition;

    void Start()
    {
        move = false;
    }

    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition.position, speed * Time.deltaTime);
        }
    }

    public void Interact()
    {
        move = true;
    }
}
