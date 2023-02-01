/*********************************
* File Name: LaserBehaviour
* 
* Author: Shane Hajny
* Minor Author: Jess Peters
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
    public LineRenderer lr;

    /// <summary>
    /// Holds a reference to the most recently touched crawlspace by the laser.
    /// </summary>
    private GameObject mostRecentCrawlspace;

    //Currently needs to be setup in scene view
    public GameObject dot;

    public string reflect = "Reflective";
    public List<Vector3> laserHits = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cb = GetComponent<CameraBehavior>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //If holding left mouse button
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray;
            RaycastHit hit;

            
            ray = cam.ScreenPointToRay(Input.mousePosition);
            
           

            laserHits.Clear();
            laserHits.Add(cam.transform.position - (cam.transform.up * 0.5f));

            if (Physics.Raycast(ray, out hit))
            {
                lr.enabled = true;
                laserHits.Add(hit.point);

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

                //if dot hits a crawlspace
                if (hit.collider.gameObject.CompareTag("Crawlspace"))
                {
                    mostRecentCrawlspace = hit.collider.gameObject;
                    mostRecentCrawlspace.GetComponent<CrawlspaceBehavior>().isIndicated = true;

                }
                else
                {
                    if (mostRecentCrawlspace != null)
                    {
                        mostRecentCrawlspace.GetComponent<CrawlspaceBehavior>().isIndicated = false;
                        mostRecentCrawlspace = null;

                    }
                }


                for (int x = 0; x < laserHits.Count; x++)
                {
                    lr.positionCount = laserHits.Count;
                    lr.SetPosition(x, laserHits[x]);
                }
            }
        }
        else
        {
            dot.transform.position = new Vector3(0, -100, 0);

            if (lr.enabled == true)
            {
                lr.enabled = false;
            }
        }
    }

    public void ReflectDot(Vector3 inDir, Vector3 normal, Vector3 reflectPoint)
    {
        Ray ray = new Ray(reflectPoint, Vector3.Reflect(inDir, normal));
        RaycastHit hit;

        //Just the raycast setup from before
        if (Physics.Raycast(ray, out hit))
        {
            laserHits.Add(hit.point);

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
