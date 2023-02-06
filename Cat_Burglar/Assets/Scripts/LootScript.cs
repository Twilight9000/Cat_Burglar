/* Author: Parker DeVenney
 * Jess Peters
 * Date: 1/30/23
 * Description: Contains info about the loot
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    [Tooltip("Cat max carry weight is 7. Smallest item can be is a 1.")]
    public int weight = 1;

    [Tooltip("The points the player gets for successfully stealing this item.")]
    public float moneyValue = 0;

    [Tooltip("If true, this items should trigger the shutdown of the museum.")]
    public bool triggersShutdown = false;

    [Tooltip("DO NOT CHANGE ME IN INSPECTOR. IS FOR READING VALUES ONLY.\nSet to true when the item is stolen.")]
    public bool isStolen = false;

    public Rigidbody myRB;

    private GameController gc;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();

        myRB = gameObject.GetComponent<Rigidbody>();
        isStolen = false;
    }

    private void Update()
    {
      //  Debug.Log(isStolen);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision...");
            isStolen = true;
            
            if (triggersShutdown)
            {
                ShutdownTriggered();
            }
            
            
            //NOTICE: KEEP THIS AT THE BOTTOM OF THE SCRIPT
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Called when the museum is shut down. This tells the guards and cameras to deploy.
    /// </summary>
    void ShutdownTriggered()
    {
        gc.ShutdownActivated();

        //TODO: Tell gameController that guards need to spawn and stuff.

    }

}
