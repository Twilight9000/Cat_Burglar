/* Author: Parker DeVenney
 * File Name: MainMenuBehaviour.cs
 * Date: 2/6/23
 * Description: Manages main menu button functions 
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    /// <summary>
    /// Changes the scene
    /// </summary>
    /// <param name="nameOfScene"> String of scene name </param>
    public void ChangeScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }

    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
