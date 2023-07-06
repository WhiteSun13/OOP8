using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    public float radius = 1f;
    private Vector2 startPosition;
    private bool flyDown = false;
    private bool circleMove = false;
    private float angle;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (animator.GetBool("Death") == false)
        {
            transform.position = startPosition;
            rb.velocity = new Vector2(0, 0);
            if (Physics2D.OverlapCircle(this.transform.position, 16f, LayerMask.GetMask("Player"))) FindObjectOfType<AudioManager>().Play("BirdFly");
            yield return new WaitForSeconds(1);
            flyDown = true;
            circleMove = false;
            yield return new WaitForSeconds(0.5f);
            flyDown = false;
            circleMove = true;
            yield return new WaitForSeconds(1.4f);
            flyDown = true;
            circleMove = false;
            yield return new WaitForSeconds(2);
            flyDown = false;
        }
    }
    void Update()
    {
        if (circleMove && animator.GetBool("Death") == false)
        {
            angle += Time.deltaTime;
            rb.velocity = new Vector2(-Mathf.Cos(angle * speed/2) * radius, Mathf.Sin(angle * speed/2) * radius);
        }
        if (flyDown && animator.GetBool("Death") == false)
        {
            angle = 0;
            rb.velocity = new Vector2(-speed, -speed);
        }
    }

    public override void TakeDamage()
    {
        StopCoroutine(Move());
        flyDown = false;
        circleMove = false;
        base.TakeDamage();
    }
}
