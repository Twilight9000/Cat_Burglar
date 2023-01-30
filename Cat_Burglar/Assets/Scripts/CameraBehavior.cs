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
    [Tooltip("DO NOT CHANGE ME. IS FOR READING VALUES ONLY")]
    public float rotFromRelativeZero;


    private int ventSelected = 0;
   // public List<GameObject> ventsList;
    public GameObject[] ventsList;

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
        ventsList = GameObject.FindGameObjectsWithTag("Vent");
        ventSelected = 0;

        //TODO: set orenitation via script 

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UponEnteringAVent();

        //  transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
        //  transform.rotation = Quaternion.Euler(ventsList[ventSelected].GetComponent<Transform>().rotation.x, ventsList[ventSelected].GetComponent<Transform>().rotation.y, ventsList[ventSelected].GetComponent<Transform>().rotation.z);

        ////  transform.position = Vector3.zero;
        // // transform.rotation = Quaternion.Euler(0, 0, 0);


        //  currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
        //  currentVentXBoundLower = currentVentScript.xLowerBound;
        //  currentVentYBoundLower = currentVentScript.yLowerBound;
        //  currentVentYBoundUpper = currentVentScript.yUpperBound;
        //  currentVentXBoundUpper = currentVentScript.xUpperBound;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.S))
        {
            ventSelected++;

            if (ventSelected > ventsList.Length - 1)
            {
                ventSelected = 0;
            }

            UponEnteringAVent();
          //  transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
          //  transform.rotation = ventsList[ventSelected].GetComponent<Transform>().rotation;


            //currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
            //currentVentXBoundLower = currentVentScript.xLowerBound;
            //currentVentYBoundLower = currentVentScript.yLowerBound;
            //currentVentYBoundUpper = currentVentScript.yUpperBound;
            //currentVentXBoundUpper = currentVentScript.xUpperBound;

            //if (currentVentScript.positiveRange)
            //{
            //    transform.rotation = Quaternion.Euler(transform.rotation.x, 190, 0);
            //}
            //else
            //{
            //    transform.rotation = Quaternion.Euler(transform.rotation.x, -10, 0);
            //}
        }

        float relativeZero = ventsList[ventSelected].transform.rotation.eulerAngles.y;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRot += mouseX;

        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, currentVentXBoundLower, currentVentXBoundUpper);

        //
        //jfc i have to write my own clamp i think oh no

        float modifiedYLower;
        float modifiedYUpper;

        //used to read the relative degree amount im the inspector.
        if (currentVentScript.looksToZero)
        {
            rotFromRelativeZero = Mathf.Abs(relativeZero - transform.rotation.eulerAngles.y);

             modifiedYLower = relativeZero - currentVentYBoundLower;
             modifiedYUpper = relativeZero + currentVentYBoundUpper;
        }
        else
        {
            rotFromRelativeZero = Mathf.Abs(relativeZero - transform.rotation.eulerAngles.y);

             modifiedYLower = relativeZero - currentVentYBoundLower;
             modifiedYUpper = 360 - (currentVentYBoundUpper - relativeZero);
        }


        yRot = Mathf.Clamp(yRot, modifiedYLower, modifiedYUpper);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        

    }

    /// <summary>
    /// This is just to store duplicate code mostly, it happens at the start and every time a vent is switched to. 
    /// </summary>
    void UponEnteringAVent()
    {

        transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
        transform.rotation = Quaternion.Euler(ventsList[ventSelected].GetComponent<Transform>().rotation.x, ventsList[ventSelected].GetComponent<Transform>().rotation.y, ventsList[ventSelected].GetComponent<Transform>().rotation.z);

        //  transform.position = Vector3.zero;
        // transform.rotation = Quaternion.Euler(0, 0, 0);


        currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
        currentVentXBoundLower = currentVentScript.xLowerBound;
        currentVentYBoundLower = currentVentScript.yLowerBound;
        currentVentYBoundUpper = currentVentScript.yUpperBound;
        currentVentXBoundUpper = currentVentScript.xUpperBound;

        if (currentVentScript.looksToZero)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 189, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -1, 0);
        }

    }



}

