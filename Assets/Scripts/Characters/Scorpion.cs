using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Scorpion : Enemy
{
    public Transform[] points;

    public float timeStopMove;
    private float timeMove;
    private int i;

    public GameObject bullet;
    public Transform shotSpawn;
    public float playerDetectRange;
    public LayerMask player;

    void Update()
    {
        if (!animator.GetBool("Death")) Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
                timeMove = timeStopMove;
                Attack();
            }
        }
        if (timeMove <= 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(1));

            if (transform.position.x <= points[i].position.x && !facingRight)
            {
                Flip();
            }
            else if (transform.position.x >= points[i].position.x && facingRight)
            {
                Flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(0));
            timeMove -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (Physics2D.OverlapCircle(this.transform.position, playerDetectRange, player) && !animator.GetBool("Death"))
        {
            if (FindObjectOfType<Player>().transform.position.x < this.transform.position.x && facingRight)
            {
                Flip();
            }
            else if (FindObjectOfType<Player>().transform.position.x > this.transform.position.x && !facingRight)
            {
                Flip();
            }
            FindObjectOfType<AudioManager>().Play("EnemyShoot");
            if (transform.localScale.x > 0) Instantiate(bullet, shotSpawn.position, Quaternion.Euler(0f, 0f, 0f));
            else Instantiate(bullet, shotSpawn.position, Quaternion.Euler(0f, 0f, 180f));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, playerDetectRange);
    }
}
