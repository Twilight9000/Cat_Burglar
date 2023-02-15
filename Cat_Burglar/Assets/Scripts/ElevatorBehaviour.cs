using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour
{

    public GameObject platform;
    public GameObject topButton;
    public GameObject bottomButton;

    public Vector3 top;
    public Vector3 bot;
    public Vector3 current;
    public Vector3 direction;

    public int speed;

    public void Update()
    {
        Vector3 clamping = platform.transform.position;
        clamping.y = Mathf.Clamp(platform.transform.position.y, bot.y, top.y);
        platform.transform.position = clamping;

        if ((direction == new Vector3(0, 1, 0) && platform.transform.position.y >= top.y) || (direction == new Vector3(0, -1, 0) && platform.transform.position.y <= bot.y))
        {

            CancelInvoke("MovePlatform");

        }
        else if (direction == new Vector3(0, 1, 0) && platform.transform.position.y != top.y)
        {

            current = top;
            Invoke("MovePlatform", 0);

        }
        else if (direction == new Vector3(0, -1, 0) && platform.transform.position.y != bot.y)
        {

            current = bot;
            Invoke("MovePlatform", 0);

        }

    }

    public void MovePlatform()
    {

        platform.transform.localPosition = Vector3.MoveTowards(platform.transform.localPosition, current, speed * Time.deltaTime);

    }

}
