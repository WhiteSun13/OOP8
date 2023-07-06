using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void Move(float horizontalInput);
    void TakeDamage();
}

public abstract class Character : MonoBehaviour, ICharacter
{

    protected Rigidbody2D rb;
    protected internal Animator animator;
    protected bool facingRight = true;
    [SerializeField] protected float speed;

    public abstract void TakeDamage();

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GetComponent<Animator>() != null) animator = GetComponent<Animator>();
    }

    public virtual void Move(float move)
    {
        if (rb.bodyType != RigidbodyType2D.Static)
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
