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
    public int currentControls = 1;
    public float sensitivity = 1000f;
    public float xRot;
    public Transform playerBody;
    private int ventSelected = 0;
    public List<GameObject> ventsList;
    public GameObject fpsCanvas;

    public float currentVentXBoundUpper;
    public float currentVentYBoundUpper;
    public float currentVentXBoundLower;
    public float currentVentYBoundLower;

    private PointBehavior currentVentScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentControls = 1;
            transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
            transform.rotation = ventsList[ventSelected].GetComponent<Transform>().rotation;
            fpsCanvas.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentControls = 2;
            transform.position = playerBody.position;
            fpsCanvas.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentControls = 3;
            playerBody.position = ventsList[ventSelected].GetComponent<Transform>().position;
            playerBody.rotation = ventsList[ventSelected].GetComponent<Transform>().rotation;
            transform.position = playerBody.position;
            fpsCanvas.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
            currentVentXBoundLower = currentVentScript.xLowerBound;
            currentVentYBoundLower = currentVentScript.yLowerBound;
            currentVentYBoundUpper = currentVentScript.yUpperBound;
            currentVentXBoundUpper = currentVentScript.xUpperBound;

        }



        switch (currentControls)
        {
            case 1:
                Scheme1();

                break;

            case 2:
                Scheme2();
                
                break;

            case 3:
                Scheme3();
                break;

            default:
                currentControls = 1;
                break;

        }
    }

    /// <summary>
    /// The camera behaviors for the Vents control scheme.
    /// </summary>
    void Scheme1()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ventSelected++;

            if (ventSelected > ventsList.Count - 1)
            {
                ventSelected = 0;
            }

            transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
            transform.rotation = ventsList[ventSelected].GetComponent<Transform>().rotation;

        }

    }

    /// <summary>
    /// The camera behaviors for the FPS control scheme.
    /// </summary>
    void Scheme2()
    {
        float MX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float MY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= MY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * MX);

    }


    /// <summary>
    /// The combined version.
    /// </summary>
    void Scheme3()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ventSelected++;

            if (ventSelected > ventsList.Count - 1)
            {
                ventSelected = 0;
            }

            playerBody.position = ventsList[ventSelected].GetComponent<Transform>().position;
            playerBody.rotation = ventsList[ventSelected].GetComponent<Transform>().rotation;


            currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
            currentVentXBoundLower = currentVentScript.xLowerBound;
            currentVentYBoundLower = currentVentScript.yLowerBound;
            currentVentYBoundUpper = currentVentScript.yUpperBound;
            currentVentXBoundUpper = currentVentScript.xUpperBound;


        }

        float MX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float MY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= MY;
        xRot = Mathf.Clamp(xRot, currentVentXBoundLower, currentVentXBoundUpper);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * MX);

    }

}

