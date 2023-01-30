/* Author: Jess Peters
 * Date: 1/25/23
 * Description: Each of these contains a set of bounds for how far the camera is allowed to rotate.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehavior : MonoBehaviour
{
    public float xLowerBound;
    public float yLowerBound;
    public float xUpperBound;
    public float yUpperBound;

    [Tooltip("Set true if 0 is the direction the camera looks in general. Else set false.")]
    public bool looksTo0 = false;

    [Tooltip("Set true if 90 is the direction the camera looks in general. Else set false.")]
    public bool looksTo90 = false;

    [Tooltip("Set true if 180/-180 is the direction the camera looks in general. Else set false.")]
    public bool looksTo180 = false;

    [Tooltip("Set true if -90 is the direction the camera looks in general. Else set false.")]
    public bool looksToNegative90 = false;

}
