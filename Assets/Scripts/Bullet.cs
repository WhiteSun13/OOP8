using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timeToDestroy;
    protected Rigidbody2D rb;
    protected Vector2 startPosition;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        startPosition = transform.position;
        Destroy(gameObject, timeToDestroy);
    }

    public virtual void TakeDamage()
    {
        if (GetComponent<CircleCollider2D>() != null) GetComponent<CircleCollider2D>().enabled = false;
        else if (GetComponent<BoxCollider2D>() != null) GetComponent<BoxCollider2D>().enabled = false;
        if (startPosition.x > transform.position.x) rb.velocity = new Vector2(speed, -speed);
        else rb.velocity = new Vector2(-speed, -speed);
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z - 180f);
    }
}
