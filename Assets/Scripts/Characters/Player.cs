using Cinemachine;
using System.Collections;
using UnityEngine;

interface IPlayer
{
    void Jump();
    void Attack();
}

public class Player : Character, IPlayer
{
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private bool isGrounded = false;
    private float groundRadius = 0.15f;

    public float gravScale = 2f;
    private bool jumping = false;
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;
    private float jumpTime;
    public float jumpControlTime;
    private bool jumpCancelled;

    public Transform attackPos;
    public LayerMask enemy;
    public LayerMask bullet;
    public float attackRange;
    private bool mayTakeDamage = true;

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || isGrabbing))
        {
            if (isGrabbing)
            {
                isGrabbing = false;
                animator.SetBool("CanMove", true);
                rb.gravityScale = gravScale;
            }
            FindObjectOfType<AudioManager>().Play("LionJump");
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
    }

    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage();
        }

        Collider2D[] bullets = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bullet);
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].GetComponent<Bullet>().TakeDamage();
        }
    }

    private void Update()
    {
        if (!jumping) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        else isGrounded = false;
        animator.SetBool("Ground", isGrounded);
        animator.SetBool("isGrabbing", isGrabbing);
        if (animator.GetBool("Attacked")) Attack();
        LedgeGrab();
    }

    private void FixedUpdate()
    {
        if (jumpCancelled && jumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && mayTakeDamage)
        {
            TakeDamage();
        }
    }

    public override void TakeDamage()
    {
        mayTakeDamage = false;
        animator.SetTrigger("TakeDamage");
        animator.SetBool("CanMove", false);
        GameManager.Sethealth(-1);
        FindObjectOfType<AudioManager>().Play("LionDamage");
        if (GameManager.health>0) StartCoroutine(Invicible());
        else
        {
            rb.velocity = new Vector2(0, 0);
            FindObjectOfType<CinemachineVirtualCamera>().gameObject.SetActive(false);
            animator.SetBool("Death", true);
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            rb.mass = 0.1f;
            rb.gravityScale = gravScale;
            Invoke("GameOver", 3f);
        }
    }

    void GameOver() => LevelController.levelController.GameOver();

    IEnumerator Invicible()
    {
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.4f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        for (int i = 0; i < 20; i++)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.1f);
        }
        mayTakeDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform") && isGrounded)
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            transform.parent = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector2(transform.position.x + (wallUpXOffset * transform.localScale.x), transform.position.y + (wallUpYOffset * transform.localScale.y)), new Vector2(transform.localScale.x/Mathf.Abs(transform.localScale.x) * wallUpSize, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(transform.position.x + (wallDownXOffset * transform.localScale.x), transform.position.y + (wallDownYOffset * transform.localScale.y)), new Vector2(transform.localScale.x / Mathf.Abs(transform.localScale.x) * wallDownSize, 0));
    }

    [Header("Ledge Grab")]
    private bool isGrabbing;
    private bool onWallUp;
    private bool onWallDown;
    public float wallUpXOffset, wallUpYOffset;
    public float wallUpSize;
    public float wallDownXOffset, wallDownYOffset;
    public float wallDownSize;
    public LayerMask wallLedge;
    public LayerMask wallCantClimb;
    private bool cantClimb;
    private void LedgeGrab()
    {
        onWallUp = Physics2D.Raycast(new Vector2(transform.position.x + (wallUpXOffset * transform.localScale.x), transform.position.y + (wallUpYOffset * transform.localScale.y)), new Vector2(transform.localScale.x, 0), wallUpSize, wallLedge);
        onWallDown = Physics2D.Raycast(new Vector2(transform.position.x + (wallDownXOffset * transform.localScale.x), transform.position.y + (wallDownYOffset * transform.localScale.y)), new Vector2(transform.localScale.x, 0), wallDownSize, wallLedge);
        cantClimb = Physics2D.Raycast(new Vector2(transform.position.x + (wallDownXOffset * transform.localScale.x), transform.position.y + (wallDownYOffset * transform.localScale.y)), new Vector2(transform.localScale.x, 0), wallDownSize, wallCantClimb);

        if (((onWallDown && !onWallUp) || cantClimb) && !isGrounded && !isGrabbing && !jumping)
        {
            isGrabbing = true;
            FindObjectOfType<AudioManager>().Play("LedgeGrab");
            Input.ResetInputAxes();
        }

        if (isGrabbing)
        {
            animator.SetBool("CanMove", false);
            rb.velocity = new Vector2(0f, 0f);
            rb.gravityScale = 0f;
        }

        if (isGrabbing && !onWallUp && !cantClimb && (Input.GetAxisRaw("Horizontal") * transform.localScale.x) > 0)
        {
            animator.SetTrigger("GrabbingUp");
            transform.position = new Vector2(transform.position.x + (0.1f * transform.localScale.x), transform.position.y + 1f);
            rb.gravityScale = gravScale;
            isGrabbing = false;
        }

        if (isGrounded) animator.ResetTrigger("GrabbingUp");
    }
}
