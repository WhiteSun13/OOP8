using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    public Transform[] points;

    public float timeStopMove;
    private float timeMove;
    private int i;
    
    void Update()
    {
        if (!animator.GetBool("Death")) Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            if (i == 0)
            {
                i = 1;
                timeMove = timeStopMove;
            }
            else if (i == 1)
            {
                i = 0;
                timeMove = timeStopMove;
                animator.SetTrigger("Attack");
            }
        }
        if (timeMove <= 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(1));
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(0));
            timeMove -= Time.deltaTime;
        }
    }
}
