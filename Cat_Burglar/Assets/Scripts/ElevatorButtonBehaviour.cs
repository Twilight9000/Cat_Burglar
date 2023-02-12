using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButtonBehaviour : MonoBehaviour
{

    public GameObject platform;

    public Vector3 moveHere;

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Dot")
        {

            platform.GetComponent<ElevatorBehaviour>().direction = moveHere;

            platform.GetComponent<ElevatorBehaviour>().MovePlatform();

        }

    }

}
