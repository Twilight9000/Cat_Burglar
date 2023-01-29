/* Author: Parker DeVenney
 * File Name: TestMoveBehaviour.cs
 * Date: 1/25/23
 * Description: Little movement behaviour for testing a pause screen. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveBehaviour : MonoBehaviour
{
    int speed = 5;

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
