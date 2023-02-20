/*********************************
* File Name: SecurityCamBehavior
* 
* Author: Shane Hajny
* 
* Summary: If the cat is not at
* too wide of an angle from the
* camera and not obscured, this
* reports the cat's location to
* the security guards.
*********************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityCamBehavior : MonoBehaviour, IShinable
{
    public GameController gc;
    public GameObject cat;
    public Light spLight;
    public float maxAngle;
    public float camRange;

    public float disableTime;

    public GameObject[] guards;

    // Start is called before the first frame update
    void Awake()
    {
        cat = GameObject.FindGameObjectWithTag("Player");
        spLight = transform.GetChild(0).gameObject.GetComponent<Light>();
        spLight.spotAngle = maxAngle * 2;
        spLight.range = camRange + 5;
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit hit;

        if (Vector3.Angle(transform.forward, cat.transform.position - transform.position) < maxAngle)
        {
            if (Physics.Raycast(transform.position, cat.transform.position - transform.position, out hit, camRange))
            {
                if (hit.collider.gameObject == cat)
                {
                    Debug.Log("hi");
                    for (int x = 0; x < guards.Length; x++)
                    {
                        guards[x].GetComponent<NavMeshAgent>().destination = GameObject.Find("Origami_Cat_Model").transform.position;
                    }
                }
            }
        }
        /*else
        {
            for (int x = 0; x < guards.Length; x++)
            {
                guards[x].GetComponent<NavMeshAgent>().destination = null;
            }
        }*/
    }

    public IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(disableTime);

        spLight.enabled = true;
    }

    public void Disable()
    {
        spLight.enabled = false;
        StartCoroutine("DisableTimer");
    }
}
