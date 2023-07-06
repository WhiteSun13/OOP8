using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;

    public float timeStopMove = 0;
    private float timeMove;
    public float speed;
    private int i;

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            timeMove = timeStopMove;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        if (timeMove <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
            timeMove -= Time.deltaTime;
        }
    }
}
