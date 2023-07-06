using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    public override void TakeDamage()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        if (animator != null)animator.SetBool("Death", true);
        FindObjectOfType<AudioManager>().Play("EnemyDie");
        rb.velocity = new Vector2(0, 0);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 0.1f;
        rb.gravityScale = 1.5f;
        rb.AddForce(new Vector2(10, 20));
        Destroy(gameObject,3f);
    }
}
