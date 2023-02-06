using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatBehaviour : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    public NavMeshAgent nAgent;
    public const int MAX_ITEMS_CARRY = 7;
    public int currentCarriedWeight = 7;
    private const int MIN_NAV_AGENT_SPEED = 2;
    private const int MIN_NAV_ANGLE_AND_ACCEL = 121;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Start()
    {
        nAgent = GetComponent<NavMeshAgent>();
    }

    public void ChangeCarryWeight()
    {
        nAgent.speed = currentCarriedWeight + MIN_NAV_AGENT_SPEED;
        nAgent.angularSpeed = currentCarriedWeight * 34 + MIN_NAV_ANGLE_AND_ACCEL;
        nAgent.acceleration = currentCarriedWeight * 34 + MIN_NAV_ANGLE_AND_ACCEL;
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshHit hit;
        if(Input.GetMouseButton(0))
        {
            if (!(nAgent.Raycast(GameObject.Find("T H E D O T").transform.position, out hit)))
            {
                target = GameObject.Find("T H E D O T").transform;
            }
            else if ((Vector3.Distance(GameObject.Find("T H E D O T").transform.position, transform.position)) < 7)
            {
                target = GameObject.Find("T H E D O T").transform;
            }
            else
            {
                target = null;
            }
        }
        else
        {
            if (nAgent.isOnNavMesh)
            {
                target = null;
            }
        }

        if(!(target == null))
        {
            nAgent.isStopped = false;
            destination = target.transform.position;
            nAgent.destination = destination;
        }
        else
        {
            if (!nAgent.isOnOffMeshLink)
            {
                nAgent.isStopped = true;
            }
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
