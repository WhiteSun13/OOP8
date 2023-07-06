using UnityEngine;

public class Bear : Enemy
{
    public bool playSound = true;
    public Transform[] points;

    public float timeStopMove;
    private float timeMove;
    private int i;

    void Update()
    {
        if (!animator.GetBool("Death")) Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i != points.Length && Physics2D.OverlapCircle(this.transform.position, 15f, LayerMask.GetMask("Player")) && playSound) FindObjectOfType<AudioManager>().Play("BearMove");
            if (i == points.Length)
            {
                i = 0;
                timeMove = timeStopMove;
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
            timeMove -=Time.deltaTime;
        }
    }
}
