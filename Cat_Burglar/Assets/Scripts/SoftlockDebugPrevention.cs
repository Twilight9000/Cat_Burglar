using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftlockDebugPrevention : MonoBehaviour
{
    [Tooltip("The lowest y point; if the cat is below this point, teleport up.")]
    public float lowestPoint;

    [Tooltip("The y value that the cat reappears at if lowestPoint is passed.")]
    public float teleportHeight;


    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < lowestPoint)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, teleportHeight, gameObject.transform.position.z);
        }
        
    }
}
