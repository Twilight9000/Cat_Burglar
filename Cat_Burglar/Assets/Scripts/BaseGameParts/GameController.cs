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
    public Text totalMoneyText, moneyCarriedText;

    /// <summary>
    /// A list of the guards in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] guardsList;

    /// <summary>
    /// A list of the security cameras in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] securityCamerasList;

    /// <summary>
    /// List of red lights in the game. Auto-populates at Start().
    /// </summary>
    private GameObject[] redLightsList;

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
        redLightsList = GameObject.FindGameObjectsWithTag("Red Light");
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

        foreach (GameObject redlLight in redLightsList)
        {
            redlLight.SetActive(false);
        }

    }

    /// <summary>
    /// Allows pausing and ccycling of cat cam and minimap
    /// </summary>
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
    /// TODO: what
    /// </summary>
    /// <returns></returns>
    IEnumerator DeactivateWinText()
    {
        yield return new WaitForSeconds(5f);
        WinText.SetActive(false);
    }
    
    /// <summary>
    /// TODO: what
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
        totalMoneyText.text = "$ Gained: " + TextChange(totalMoneyScore);
        moneyCarriedText.text = "$ Carried " + TextChange(moneyCaried);
    }


    string TextChange(float money)
    {
        if (totalMoneyScore / 1000000000 >= 1)
        {
            return (money / 1000000000).ToString("F2") + "Bil";
        }
        else if (totalMoneyScore / 1000000 >= 1)
        {
            return (money / 1000000).ToString("F2") + "Mil";
        }
        else if (totalMoneyScore / 1000 >= 1)
        {
            return (money / 1000).ToString("F2") + "K";
        }
        return money.ToString();
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
    /// Activates the guards, cameras, and red lights within the scene.
    /// </summary>
    public void ShutdownActivated()
    {
        foreach(GameObject guard in guardsList)
        {
            guard.SetActive(true);
        }

        foreach(GameObject securityCam in securityCamerasList)
        {
            securityCam.SetActive(true);
        }

        foreach (GameObject redlLight in redLightsList)
        {
            redlLight.SetActive(true);
        }

    }


}
