using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private float _horizontalInput;
    private float attackRangeTemp;

    private void Start()
    {
        player = GetComponent<Player>();
        attackRangeTemp = player.attackRange;
    }

    private void Update()
    {
        if (!Pause.isPaused)
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            player.Jump();

            if (Input.GetKeyDown(KeyCode.LeftShift) && (player.animator.GetBool("CanMove")))
            {
                if (player.animator.GetBool("Ground")) player.animator.SetBool("CanMove", false);
                player.animator.SetTrigger("Attack");
                FindObjectOfType<AudioManager>().Play("LionAttack");
                player.animator.SetBool("Attacked", true);
            }

            if (Input.GetKeyDown(KeyCode.W) && player.animator.GetBool("Ground"))
            {
                player.animator.SetBool("LookUp", true);
                player.attackRange = player.attackRange * 1.25f;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                player.animator.SetBool("LookUp", false);
                player.attackRange = attackRangeTemp;
            }

            if (Input.GetKeyDown(KeyCode.S) && player.animator.GetBool("Ground"))
            {
                player.animator.SetBool("LookDown", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                player.animator.SetBool("LookDown", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (player.animator.GetBool("CanMove")) player.Move(_horizontalInput);
        else player.Move(0f);
    }
}