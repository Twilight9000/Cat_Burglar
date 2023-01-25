using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public Camera cam;
    public CameraBehavior cb;

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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray;
            RaycastHit hit;

            if (cb.currentControls == 1 || cb.currentControls == 3)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = new Ray(cam.transform.position, cam.transform.forward);
            }

            if (Physics.Raycast(ray, out hit))
            {
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
        else
        {
            dot.transform.position = new Vector3(0, -100, 0);
        }
    }

    public void ReflectDot(Vector3 inDir, Vector3 normal, Vector3 reflectPoint)
    {
        Ray ray = new Ray(reflectPoint, Vector3.Reflect(inDir, normal));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
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
