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
    public float maxAngle;
    public float range;

    // Start is called before the first frame update
    void Start()
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
            if (Physics.Raycast(transform.position, dot.transform.position - transform.position, out hit, range) && hit.collider.gameObject == dot)
            {
                dot.tag = "Untagged";
            }
        }
        else if (dot.CompareTag("Untagged"))
        {
            dot.tag = "Dot";
        }
    }
}
