/* Author: Parker DeVenney
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
    public GameObject PauseMenu;

    private void Start()
    {
        PauseMenu.SetActive(false);
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
        }
        else
        {
            PauseMenu.SetActive(true);
        }
    }

    public void ChangeScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("QUIT GAME!");
    }
}
