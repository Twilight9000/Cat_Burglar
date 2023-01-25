/*********************************
* File Name: LaserBehaviour
* 
* Author: Shane Hajny
* 
* Summary: When holding left mouse
* button down, moves the dot to
* where the player directs it.
*********************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    //Necessary initializers
    public Camera cam;
    public CameraBehavior cb;

    //Currently needs to be setup in scene view
    public GameObject dot;

    public string reflect = "Reflective";

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cb = GetComponent<CameraBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        //If holding left mouse button
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray;
            RaycastHit hit;

            //If in the locked camera positions...
            if (cb.currentControls == 1 || cb.currentControls == 3)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
            }
            //If in first person...
            else
            {
                ray = new Ray(cam.transform.position, cam.transform.forward);
            }

            if (Physics.Raycast(ray, out hit))
            {
                //If reflection is to occur
                if (hit.collider.gameObject.CompareTag(reflect))
                {
                    ReflectDot(ray.direction, hit.normal, hit.point);
                }
                //Else, move dot to initial position
                else
                {
                    dot.transform.position = hit.point;
                }
            }
        }
        else
        {
            dot.transform.position = new Vector3(0, -100, 0);
        }
    }

    public void ReflectDot(Vector3 inDir, Vector3 normal, Vector3 reflectPoint)
    {
        Ray ray = new Ray(reflectPoint, Vector3.Reflect(inDir, normal));
        RaycastHit hit;

        //Just the raycast setup from before
        if (Physics.Raycast(ray, out hit))
        {
            //Except it loops here if it should reflect again
            if (hit.collider.gameObject.CompareTag(reflect))
            {
                ReflectDot(ray.direction, hit.normal, hit.point);
            }
            else
            {
                dot.transform.position = hit.point;
            }
        }
    }
}
