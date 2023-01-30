/* Author: Jess Peters
 * Date: 1/30/23
 * Description: Contains the camera controls. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlspaceBehavior : MonoBehaviour
{
    [Tooltip("Needs to be set to the matching crawlspace that is the 'other side' of the imaginary tunnel connecting them.")]
    public GameObject matchingCrawlspace;

    /// <summary>
    /// If the laser is touching this crawlspace, set true. Otherwise should be false.
    /// </summary>
    private bool isIndicated = false;

    [Tooltip("This is where the cat appears when this crawlspace is teleported to.")]
    public Vector3 telePosition;


    /// <summary>
    /// Finds the player.
    /// </summary>
    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {         
            if (isIndicated)
            {
                //teleport cat
                collision.gameObject.transform.position = matchingCrawlspace.GetComponent<CrawlspaceBehavior>().telePosition;

            }

        }

        if (collision.gameObject.CompareTag("Dot"))
        {
            isIndicated = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dot"))
        {
            isIndicated = false;
        }


    }

}
