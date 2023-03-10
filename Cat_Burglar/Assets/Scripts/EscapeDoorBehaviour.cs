/* Author: Charlie Neville
 * Date: 2/6/2023
 * Description: Contains the escape door functionality
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeDoorBehaviour : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        //when cat touches door
        if (collision.gameObject.tag == "Player")
        {
            //loads the end scene
            PlayerPrefs.SetFloat("Money", GameObject.Find("GameController").GetComponent<GameController>().totalMoneyScore);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EndScene");
            Debug.Log("escaped");

        }

    }
}
