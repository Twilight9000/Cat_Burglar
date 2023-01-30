/* Author: Jess Peters
 * Date: 1/23/23
 * Description: Contains the camera controls. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private int ventSelected = 0;
    public List<GameObject> ventsList;

    public float currentVentXBoundUpper = 360;
    public float currentVentYBoundUpper = 360;
    public float currentVentXBoundLower = -360;
    public float currentVentYBoundLower = -360;

    public GameObject orientation;

    private PointBehavior currentVentScript;

    public float sensX = 1000f;
    public float sensY = 1000f;
    public float xRot;
    public float yRot;


    // Start is called before the first frame update
    void Start()
    {

        //TODO: set orenitation via script 

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
        transform.rotation = Quaternion.Euler(ventsList[ventSelected].GetComponent<Transform>().rotation.x, ventsList[ventSelected].GetComponent<Transform>().rotation.y, ventsList[ventSelected].GetComponent<Transform>().rotation.z);

      //  transform.position = Vector3.zero;
       // transform.rotation = Quaternion.Euler(0, 0, 0);
        

        currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
        currentVentXBoundLower = currentVentScript.xLowerBound;
        currentVentYBoundLower = currentVentScript.yLowerBound;
        currentVentYBoundUpper = currentVentScript.yUpperBound;
        currentVentXBoundUpper = currentVentScript.xUpperBound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ventSelected++;

            if (ventSelected > ventsList.Count - 1)
            {
                ventSelected = 0;
            }

          //  transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
          //  transform.rotation = ventsList[ventSelected].GetComponent<Transform>().rotation;


            currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
            currentVentXBoundLower = currentVentScript.xLowerBound;
            currentVentYBoundLower = currentVentScript.yLowerBound;
            currentVentYBoundUpper = currentVentScript.yUpperBound;
            currentVentXBoundUpper = currentVentScript.xUpperBound;
        }


        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRot += mouseX;

        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, currentVentXBoundLower, currentVentXBoundUpper);
        yRot = Mathf.Clamp(yRot, currentVentYBoundLower, currentVentYBoundUpper);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        

    }




}

