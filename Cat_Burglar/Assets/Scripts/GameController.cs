/* Author: Parker DeVenney
 * Jessica Peters
 * File Name: GameController.cs
 * Date: 1/25/23
 * Description: Handles pausing and other game events/UI. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string SceneName;
    public GameObject PauseMenu, DiamondObj, WinText;
    public DiamondBehaviour objRef;

    /// <summary>
    /// A list of the guards in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] guardsList;

    /// <summary>
    /// A list of the security cameras in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] securityCamerasList;


    /// <summary>
    /// Makes sure the pause and win items are set to false.
    /// Populates the list of cameras and guards that have been placed within the scene, so they can be activated later.
    /// </summary>
    private void Start()
    {
        PauseMenu.SetActive(false);
        WinText.SetActive(false);

        if (DiamondObj != null)
        {
            objRef = DiamondObj.GetComponent<DiamondBehaviour>();
        }

        guardsList = GameObject.FindGameObjectsWithTag("Guard");
        securityCamerasList = GameObject.FindGameObjectsWithTag("Security Camera");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameState();
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeScene(SceneName);
        }

        //if the game is broken, set to win state lol
        if (DiamondObj != null)
        {
            if(objRef.isStolen == true)
            {
                WinText.SetActive(true);
            }
        }
    }
    
    public void ChangeGameState() 
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.paused
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);

        if (PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            PauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// Changes scenes
    /// </summary>
    /// <param name="nameOfScene">Name of the scene being changed to.</param>
    public void ChangeScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    /// <summary>
    /// Quits the game lol
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        print("QUIT GAME!");
    }

    /// <summary>
    /// Activates the guards and cameras within the scene.
    /// </summary>
    public void ShutdownActivated()
    {
        foreach(GameObject a in guardsList)
        {
            //TODO: activate all guards

        }

        foreach(GameObject a in securityCamerasList)
        {
            //TODO: activate all cameras
        }

    }


}
