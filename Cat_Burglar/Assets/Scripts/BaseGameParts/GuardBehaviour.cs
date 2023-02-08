using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviour : MonoBehaviour
{
    public NavMeshAgent gnAgent;
    public Transform target;
    Vector3 destination;

    public Transform[] guardPoints;
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
        /*else
        {
            target = guardPoints[listIndex];
        }*/

        if (!(target == null))
        {
            gnAgent.isStopped = false;
            destination = target.position;
            gnAgent.destination = destination;
        }
        else
        {
            destination = guardPoints[listIndex].transform.position;
            gnAgent.destination = destination;
            if (transform.position == guardPoints[listIndex].transform.position)
            {
                listIndex++;
            }
            if (listIndex > guardPoints.Length)
            {
                listIndex = 0;
            }
        }
    }
}
