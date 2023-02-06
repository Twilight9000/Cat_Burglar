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
    [Tooltip("DO NOT CHANGE ME. IS FOR READING VALUES ONLY.\nThe Y distance rotated away from the relative center of where the camera points.")]
    public float rotFromRelativeZero;

    [Tooltip("DO NOT CHANGE ME. IS FOR READING VALUES ONLY.\nThe currently selected vent you are looking out of.")]
    public GameObject currentlySelectedVent;

    /// <summary>
    /// The currently selected vent; also should be the current index in ventsList.
    /// </summary>
    private int ventSelected = 0;

    /// <summary>
    /// A list of the vents in the game. Auto-populates upon the game starting up with every item with the "Vents" tag.
    /// </summary>
    private GameObject[] ventsList;

    [Tooltip("")]
    public float currentVentXBoundLower = -360;

    [Tooltip("")]
    public float currentVentYBoundLeft = -360;

    [Tooltip("")]
    public float currentVentXBoundUpper = 360;

    [Tooltip("")]
    public float currentVentYBoundRight = 360;

    /// <summary>
    /// The script component of the currently selected vent being looked out of.
    /// </summary>
    private PointBehavior currentVentScript;

    [Tooltip("The X input senstivity for the mouse.")]
    public float sensX = 1000f;

    [Tooltip("The Y input sensitivity for the mouse.")]
    public float sensY = 1000f;

    /// <summary>
    /// The amount being moved in the x axis.
    /// </summary>
    private float xRot;

    /// <summary>
    /// The amount being moved in the y axis.
    /// </summary>
    private float yRot;

    /// <summary>
    /// Stores the general direction that the vent is facing. The general center of where the camera should look. 
    /// </summary>
    private float relativeZero;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    /// <summary>
    /// Generates a list of all the vents in the scene, sets the cursor to the correct settings for the FPS, 
    /// then calls the method for entering a vent in order to place the player in the first vent in the list.
    /// </summary>
    void Start()
    {
        ventsList = GameObject.FindGameObjectsWithTag("Vent");
        ventSelected = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        UponEnteringAVent();

    }

    /// <summary>
    /// Checks for input to change to the next vent. 
    /// Then, handles looking around with the FPS camera.
    /// </summary>
    void Update()
    {   
        //Checks whether or not to change vents and handles what happens when the list needs to loop.
        if (Input.GetKeyDown(KeyCode.S))
        {
            ventSelected++;

            if (ventSelected > ventsList.Length - 1)
            {
                ventSelected = 0;
            }

            UponEnteringAVent();
          
        }


        //How the FPS camera looks around
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRot += mouseX;
        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, currentVentXBoundLower, currentVentXBoundUpper);

        float modifiedYLower = 0;
        float modifiedYUpper = 0;

        //used to read the y amount the camera is away from the general direction the camera is pointing in the inspector.
        rotFromRelativeZero = Mathf.Abs(relativeZero - transform.rotation.eulerAngles.y);

        //The values for the y clamp are handled differently depending on what direction relative zero is.
        if (currentVentScript.looksTo0)
        {
             modifiedYLower = relativeZero - currentVentYBoundLeft;
             modifiedYUpper = relativeZero + currentVentYBoundRight;

        }
        else if (currentVentScript.looksTo180)
        {
             modifiedYLower = relativeZero - currentVentYBoundLeft;
             modifiedYUpper = 360 - (currentVentYBoundRight - relativeZero);

        }
        else if (currentVentScript.looksTo90)
        {
            modifiedYLower = -(relativeZero - currentVentYBoundLeft);
            modifiedYUpper = relativeZero + currentVentYBoundRight;

        }
        else if (currentVentScript.looksToNegative90)
        {
            modifiedYLower = -(relativeZero - currentVentYBoundLeft);
            modifiedYUpper = (relativeZero + currentVentYBoundRight);

        }
        else
        {
            print("ERROR! The current vent does not have a direction selected. :(");

        }

        yRot = Mathf.Clamp(yRot, modifiedYLower, modifiedYUpper);

        //finally, rotate camera
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        
    }

    /// <summary>
    /// This is just to store duplicate code mostly, it happens at the start and every time a vent is switched to. 
    /// It moves the camera and ests its boundary values.
    /// </summary>
    void UponEnteringAVent()
    {

        transform.position = ventsList[ventSelected].GetComponent<Transform>().position;
        transform.rotation = Quaternion.Euler(ventsList[ventSelected].GetComponent<Transform>().rotation.x, ventsList[ventSelected].GetComponent<Transform>().rotation.y, ventsList[ventSelected].GetComponent<Transform>().rotation.z);

        currentVentScript = ventsList[ventSelected].GetComponent<PointBehavior>();
        currentVentXBoundLower = currentVentScript.xLowerBound;
        currentVentYBoundLeft = currentVentScript.yLeftBound;
        currentVentYBoundRight = currentVentScript.yRightBound;
        currentVentXBoundUpper = currentVentScript.xUpperBound;

        if (currentVentScript.looksTo0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 189, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -1, 0);
        }

        currentlySelectedVent = ventsList[ventSelected];

        relativeZero = ventsList[ventSelected].transform.rotation.eulerAngles.y;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}

