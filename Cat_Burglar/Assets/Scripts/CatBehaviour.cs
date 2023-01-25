using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatBehaviour : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent nAgent;

    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            target = GameObject.Find("T H E D O T").transform;
        }
        else
        {
            target = null;
        }

        if(!(target == null))
        {
            nAgent.isStopped = false;
            destination = target.transform.position;
            nAgent.destination = destination;
        }
        else
        {
            nAgent.isStopped = true;
        }
    }
}
