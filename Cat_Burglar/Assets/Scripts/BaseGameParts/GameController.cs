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
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public string SceneName;
    public GameObject PauseMenu, DiamondObj, WinText;
    public DiamondBehaviour objRef;
    public Text totalMoneyText, moneyCarriedText;

    /// <summary>
    /// A list of the guards in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] guardsList;

    /// <summary>
    /// A list of the security cameras in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] securityCamerasList;

    public GameObject catCam;
    public GameObject skyCam;

    public int whichOtherCam = 1;

    [Tooltip("the amount of money gained")]
    public float totalMoneyScore;

    [Tooltip("current money amount in inventory")]
    public float moneyCaried;

    /// <summary>
    /// Makes sure the pause and win items are set to false.
    /// Populates the list of cameras and guards that have been placed within the scene, so they can be activated later.
    /// </summary>
    private void Start()
    {
        if (PauseMenu != null)
        { 
            PauseMenu.SetActive(false);
        }
        WinText.SetActive(false);
        GameStateManager.Instance.SetState(GameState.Gameplay);

        guardsList = GameObject.FindGameObjectsWithTag("Guard");
        securityCamerasList = GameObject.FindGameObjectsWithTag("Security Camera");
        //catCam = GameObject.FindGameObjectWithTag("CatCam");
        //skyCam = GameObject.FindGameObjectWithTag("SkyCam");

        foreach (GameObject guard in guardsList)
        {
            guard.SetActive(false);
        }

        foreach (GameObject securityCam in securityCamerasList)
        {
            securityCam.SetActive(false);
        }
        
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

        if (Input.GetKeyDown(KeyCode.S) && whichOtherCam == 1)
        {
            catCam.SetActive(true);
            skyCam.SetActive(false);
            whichOtherCam = 2;
        }
        else if (Input.GetKeyDown(KeyCode.S) && whichOtherCam == 2)
        {
            catCam.SetActive(false);
            skyCam.SetActive(true);
            whichOtherCam = 3;
        }
        else if (Input.GetKeyDown(KeyCode.S) && whichOtherCam == 3)
        {
            catCam.SetActive(false);
            skyCam.SetActive(false);
            whichOtherCam = 1;
        }
    }

    /// <summary>
    /// TODO: what does this do
    /// </summary>
    /// <returns></returns>
    IEnumerator DeactivateWinText()
    {
        yield return new WaitForSeconds(5f);
        WinText.SetActive(false);
    }
    
    /// <summary>
    /// TODO: what does this do
    /// </summary>
    public void ChangeGameState() 
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.paused
            : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);

        if (PauseMenu != null)
        {
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
    }

    /// <summary>
    /// Updates the text in the ui to accuratley represent money
    /// </summary>
    public void UpdateText()
    {
        totalMoneyText.text = "Total Money Gained: $ " + totalMoneyScore.ToString();
        moneyCarriedText.text = "Money Carried $ " + moneyCaried.ToString();
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
        foreach(GameObject guard in guardsList)
        {
            //TODO: TEST THIS
            guard.SetActive(true);


        }

        foreach(GameObject securityCam in securityCamerasList)
        {
            //TODO: TEST THIS
            securityCam.SetActive(true);

        }

    }


}
