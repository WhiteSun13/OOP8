using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHouse : Enemy
{
    [SerializeField]
    public GameObject bullet;
    public float fireRate;
    public float playerDetectRange;
    public LayerMask player;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Attack", 1f, fireRate);
    }

    void Attack()
    {
        if (Physics2D.OverlapCircle(this.transform.position, playerDetectRange, player) && rb.bodyType != RigidbodyType2D.Dynamic)
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
    }
}
