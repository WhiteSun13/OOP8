using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Enemy
{
    public Transform[] points;
    private int i;

    public GameObject bullet;
    public Transform shotSpawn;
    public float playerDetectRange;
    public LayerMask player;

    private Vector3 difference;
    private float rotZ;

    void Update()
    {
        if (animator.GetBool("Death") == false)
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }
                if (i == 0 || i == 1) Attack();
                difference = points[i].transform.position - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            }
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void Attack()
    {
        if (Physics2D.OverlapCircle(this.transform.position, playerDetectRange, player) && !animator.GetBool("Death"))
        {
            FindObjectOfType<AudioManager>().Play("EnemyShoot");
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, playerDetectRange);
    }
}
