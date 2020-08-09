using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    private Ball ball;
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    public void MoveStart(float amount)
    {
        if (!ball.inPlay)
        {
            //Debug.Log("Ball moved : " + amount);
            ball.transform.Translate(new Vector3(amount, 0, 0));
        }

    }

    public void DragStart() 
    {
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd()
    {
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;
        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

        Vector3 launchVeclocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
        ball.Launch(launchVeclocity);
    }
}
