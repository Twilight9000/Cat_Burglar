/*********************************
* File Name: RedLightBehavior
* 
* Author: Shane Hajny
* 
* Summary: If the dot is not at
* too wide of an angle from the
* light and not obscured, this
* disables the cat's ability to
* detect the dot.
*********************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightBehavior : MonoBehaviour
{
    public GameObject dot;
    public Light spLight;
    public bool dotSeen;
    public float maxAngle;
    public float range;

    // Start is called before the first frame update
    void Awake()
    {
        dot = GameObject.Find("T H E D O T");
        spLight = transform.GetChild(0).gameObject.GetComponent<Light>();
        spLight.spotAngle = maxAngle * 2;
        spLight.range = range + 5;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Vector3.Angle(transform.forward, dot.transform.position - transform.position) < maxAngle)
        {
            if (Physics.Raycast(transform.position, dot.transform.position - transform.position, out hit, range))
            {
                if (hit.collider.gameObject == dot)
                {
                    dotSeen = true;
                }
                else
                {
                    dotSeen = false;
                }
            }
            else
            {
                dotSeen = false;
            }
        }
        else
        {
            dotSeen = false;
        }
    }
}
