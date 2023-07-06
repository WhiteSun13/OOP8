using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Enemy
{
    [SerializeField]
    public GameObject bullet;
    public float fireRate;
    public Transform shotSpawn;
    public float playerDetectRange;
    public LayerMask player;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Attack", fireRate, fireRate);
    }

    void Update()
    {
        if (!animator.GetBool("Death"))
        {
            if (FindObjectOfType<Player>().transform.position.x < this.transform.position.x && facingRight)
            {
                Flip();
            }
            else if (FindObjectOfType<Player>().transform.position.x > this.transform.position.x && !facingRight)
            {
                Flip();
            }
        }
    }

    void Attack()
    {
        if (Physics2D.OverlapCircle(this.transform.position, playerDetectRange, player) && !animator.GetBool("Death"))
        {
            animator.SetTrigger("Attack");
            Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, playerDetectRange);
    }
}
