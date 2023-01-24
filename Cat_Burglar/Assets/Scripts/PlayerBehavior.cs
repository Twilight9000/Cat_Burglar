/* Author: Jess Peters
 * Date: 1/23/23
 * Description: Contains the controls for the different control schemes. 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private int currentControls = 1;
    public Camera mainCam;
    private CameraBehavior camScript;
    private Vector3 middlePos;

    // Start is called before the first frame update
    void Start()
    {
        middlePos = transform.position;
        camScript = mainCam.GetComponent<CameraBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentControls = 1;
            camScript.currentControls = 1;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentControls = 2;
            transform.position = middlePos;
            camScript.currentControls = 2;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentControls = 3;
            transform.position = middlePos;
            camScript.currentControls = 3;
        }
        */

       /* switch (currentControls)
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
                
        }     */    
    }
/*
    /// <summary>
    /// The player behaviors for the Vents control scheme.
    /// </summary>
    void Scheme1()
    {

    }

    /// <summary>
    /// The player behaviors for the FPS control scheme.
    /// </summary>
    void Scheme2()
    {
        

    }

    /// <summary>
    /// The player behaviors for the Point-And-Click control scheme.
    /// </summary>
    void Scheme3()
    {

    }


    */

}
