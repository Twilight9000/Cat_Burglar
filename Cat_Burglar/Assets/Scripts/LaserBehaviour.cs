using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public Camera cam;
    public CameraBehavior cb;

    public GameObject dot;

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
                dot.transform.position = hit.point;
            }
        }
        else
        {
            dot.transform.position = new Vector3(0, -100, 0);
        }
    }
}
