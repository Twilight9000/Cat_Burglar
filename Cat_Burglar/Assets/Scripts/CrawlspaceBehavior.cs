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

    /// <summary>
    /// Checks player collision and if line is indticating the crawlspace.
    /// </summary>
    /// <param name="collision">The object being collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {         
            if (isIndicated)
            {
                //teleport cat
                collision.gameObject.transform.parent.GetComponent<CatBehaviour>().nAgent.Warp(matchingCrawlspace.transform.right * 2 + matchingCrawlspace.transform.position);
                //collision.gameObject.transform.parent.GetComponent<CatBehaviour>().nAgent.Warp(matchingCrawlspace.GetComponent<CrawlspaceBehavior>().telePosition);
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
                collision.gameObject.transform.parent.GetComponent<CatBehaviour>().nAgent.Warp(matchingCrawlspace.transform.right * 2 + matchingCrawlspace.transform.position);
                //collision.gameObject.transform.parent.GetComponent<CatBehaviour>().nAgent.Warp(matchingCrawlspace.GetComponent<CrawlspaceBehavior>().telePosition);
            }
        }
    }


    /// <summary>
    /// Showing where the cat will teleport to if indicated to each other
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.right * 2 + transform.position, Vector3.one * 2);
    }

}
