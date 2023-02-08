using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviour : MonoBehaviour
{
    public NavMeshAgent gnAgent;
    public Transform target;
    Vector3 destination;

    public List<Transform> GuardPoints;
    public int listIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gnAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshHit hit;
        if (!(gnAgent.Raycast(GameObject.Find("Origami_Cat_Model").transform.position, out hit)) && Vector3.Distance(GameObject.Find("Origami_Cat_Model").transform.position, transform.position) < 15)
        {
            target = GameObject.Find("Origami_Cat_Model").transform;
        }
        else
        {
            target = List(listIndex).transform;
        }

        if (!(target == null))
        {
            gnAgent.isStopped = false;
            destination = target.position;
            gnAgent.destination = destination;
        }
        else
        {
            gnAgent.isStopped = true;
        }
    }
}
