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

    [Tooltip("If the laser is touching this crawlspace, set true. Otherwise should be false.")]
    public bool isIndicated = false;

     [Tooltip("This needs to be set to where the cat appears when this crawlspace is teleported to.")]
     public Vector3 telePosition;

    /// <summary>
    /// Checks player collision and if line is indicating the crawlspace.
    /// </summary>
    /// <param name="collision">The object being collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {         
            if (isIndicated)
            {
                collision.gameObject.GetComponent<CatBehaviour>().nAgent.Warp(matchingCrawlspace.GetComponent<CrawlspaceBehavior>().telePosition);
                //teleport cat
                // collision.gameObject.transform.parent.transform.position = new Vector3(matchingScript.teleX, matchingScript.teleY, matchingScript.teleZ);
              //  print("aaaaaaaaaaa");

            }
        }   
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (isIndicated)
            {
                //teleport cat
                // collision.gameObject.transform.parent.transform.position = new Vector3(matchingScript.teleX, matchingScript.teleY, matchingScript.teleZ);
                collision.gameObject.transform.parent.transform.position = matchingCrawlspace.GetComponent<CrawlspaceBehavior>().telePosition;
               // print("aaaaaaaaaaa");

            }
        }
    }

}
