using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Bullet
{
    public float radius;
    public float startAngle;
    private float angle;
    private bool death;    
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        angle = startAngle;
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        if (!death)
        {
            angle += Time.deltaTime;
            rb.velocity = new Vector2(-Mathf.Cos(angle * speed / 2) * radius, -Mathf.Sin(angle * speed / 2) * radius);
        }
    }

    public override void TakeDamage()
    {
        death = true;
        base.TakeDamage();
    }
}
