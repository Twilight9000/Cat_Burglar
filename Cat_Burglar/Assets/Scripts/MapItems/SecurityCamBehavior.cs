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

public class SecurityCamBehavior : MonoBehaviour, IShinable
{
    public GameObject cat;
    public Light spLight;
    public float maxAngle;
    public float camRange;

    public float disableTime;

    //public GuardBehavior[] guards;

    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.Find("ORIGAMI_Cat");
        spLight = transform.GetChild(0).gameObject.GetComponent<Light>();
        spLight.spotAngle = maxAngle * 2;
        spLight.range = camRange + 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Angle(transform.forward, cat.transform.position - transform.position) < maxAngle && spLight.enabled)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, cat.transform.position - transform.position, out hit, camRange))
            {
                if (hit.collider.gameObject == cat)
                {
                    /*for (int x = 0; x < guards.Length; x++)
                    {
                        guards[x].CatAlert(hit.point);
                    }*/
                }
            }
        }
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
